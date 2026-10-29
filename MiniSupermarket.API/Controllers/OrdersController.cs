using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Constants;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers;

[Authorize]
public class OrdersController : ApiControllerBase
{
    private readonly SupermarketDbContext _db;

    public OrdersController(SupermarketDbContext db) => _db = db;

    // GET: api/orders            -> toàn bộ đơn hàng
    // GET: api/orders?page=1     -> có phân trang
    [HttpGet]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> GetAll([FromQuery] int? page, [FromQuery] int pageSize = 20)
    {
        var query = _db.Orders.AsNoTracking()
            .Include(o => o.Items)
            .OrderByDescending(o => o.CreatedAt);

        if (page == null)
            return Ok((await query.ToListAsync()).Select(ToResponse));

        pageSize = Math.Clamp(pageSize, 1, 100);
        var currentPage = Math.Max(page.Value, 1);

        var totalItems = await query.CountAsync();
        var orders = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

        return Ok(PagedResult<OrderResponseDto>.Create(orders.Select(ToResponse).ToList(), totalItems, currentPage, pageSize));
    }

    // GET: api/orders/1
    [HttpGet("{id:int}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _db.Orders.AsNoTracking()
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        return order == null ? NotFoundMessage("Không tìm thấy đơn hàng!") : Ok(ToResponse(order));
    }

    // POST: api/orders/checkout (POS thanh toán)
    [HttpPost("checkout")]
    [Authorize(Roles = AppRoles.AdminOrCashier)]
    public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
    {
        if (request.Items == null || request.Items.Count == 0)
            return BadRequestMessage("Giỏ hàng trống!");

        if (request.Items.Any(i => i.Quantity <= 0))
            return BadRequestMessage("Số lượng không hợp lệ!");

        // Thu ngân lấy từ JWT, không tin dữ liệu client gửi lên
        var username = User.Identity?.Name;
        var cashier = await _db.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);
        if (cashier == null)
            return Unauthorized(new { message = "Tài khoản thu ngân không hợp lệ!" });

        Customer? customer = null;
        if (!string.IsNullOrWhiteSpace(request.CustomerPhone))
        {
            var phone = request.CustomerPhone.Trim();
            customer = await _db.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.PhoneNumber == phone);
        }

        var redeemPoints = request.RedeemPoints;
        if (redeemPoints < 0 || redeemPoints % LoyaltyRules.PointsPerRedemption != 0)
            return BadRequestMessage($"Số điểm đổi phải là bội số của {LoyaltyRules.PointsPerRedemption}!");

        if (redeemPoints > 0)
        {
            if (customer == null)
                return BadRequestMessage("Cần nhập khách hàng thân thiết để đổi điểm!");

            if (customer.RewardPoints < redeemPoints)
                return BadRequestMessage($"Khách hàng chỉ có {customer.RewardPoints} điểm!");
        }

        // Gộp các dòng trùng sản phẩm để kiểm tra tồn kho đúng
        var lines = request.Items
            .GroupBy(i => i.ProductId)
            .Select(g => (ProductId: g.Key, Quantity: g.Sum(x => x.Quantity)))
            .ToList();
        var productIds = lines.Select(l => l.ProductId).ToList();
        var products = await _db.Products.AsNoTracking()
            .Where(p => productIds.Contains(p.ProductId))
            .ToDictionaryAsync(p => p.ProductId);

        var order = new Order
        {
            CashierId = cashier.UserId,
            CustomerId = customer?.CustomerId,
            PaymentMethod = "CASH",
            Status = OrderStatus.Paid,
            CreatedAt = DateTime.Now,
            OrderCode = "TMP" + Guid.NewGuid().ToString("N")[..20] // mã tạm duy nhất, đổi sau khi có OrderId
        };

        foreach (var (productId, quantity) in lines)
        {
            if (!products.TryGetValue(productId, out var product))
                return BadRequestMessage($"Sản phẩm #{productId} không tồn tại!");

            order.Items.Add(new OrderItem
            {
                ProductId = product.ProductId,
                Quantity = quantity,
                UnitPrice = product.Price,
                LineTotal = product.Price * quantity
            });
        }

        order.Subtotal = order.Items.Sum(i => i.LineTotal);
        order.Discount = redeemPoints * LoyaltyRules.DiscountPerPoint;

        if (order.Discount > order.Subtotal)
            return BadRequestMessage("Giá trị giảm giá không được vượt quá tổng tiền hàng!");

        order.Total = order.Subtotal - order.Discount;

        if (request.CashReceived < order.Total)
            return BadRequestMessage("Tiền khách đưa chưa đủ!");

        var earnedPoints = customer != null ? RewardPointsFor(order.Total) : 0;

        // Trừ kho + đổi/tích điểm + lưu đơn + gán mã đơn trong một transaction: lỗi ở bước nào cũng rollback tất cả.
        await using var transaction = await _db.Database.BeginTransactionAsync();

        foreach (var (productId, quantity) in lines)
        {
            // Chỉ trừ khi còn đủ hàng -> an toàn khi nhiều thu ngân bán cùng lúc
            var affected = await _db.Products
                .Where(p => p.ProductId == productId && p.StockQuantity >= quantity)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.StockQuantity, p => p.StockQuantity - quantity));

            if (affected == 0)
            {
                var remaining = await _db.Products
                    .Where(p => p.ProductId == productId)
                    .Select(p => p.StockQuantity)
                    .FirstAsync();
                return BadRequestMessage($"'{products[productId].ProductName}' chỉ còn {remaining} trong kho!");
            }
        }

        if (customer != null)
        {
            // Trừ điểm đổi và cộng điểm tích lũy trong cùng transaction; chỉ thực hiện khi còn đủ điểm
            var customerId = customer.CustomerId;
            var updated = await _db.Customers
                .Where(c => c.CustomerId == customerId && c.RewardPoints >= redeemPoints)
                .ExecuteUpdateAsync(s => s.SetProperty(
                    c => c.RewardPoints, c => c.RewardPoints - redeemPoints + earnedPoints));

            if (updated == 0)
                return BadRequestMessage("Số điểm tích lũy không đủ để đổi!");
        }

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        order.OrderCode = $"HD{order.OrderId:D5}";
        await _db.SaveChangesAsync();
        await transaction.CommitAsync();

        return Ok(new CheckoutResponseDto
        {
            OrderId = order.OrderId,
            OrderCode = order.OrderCode,
            Subtotal = order.Subtotal,
            Discount = order.Discount,
            Total = order.Total,
            Change = request.CashReceived - order.Total,
            RewardPointsRedeemed = redeemPoints,
            RewardPointsEarned = earnedPoints
        });
    }

    // PUT: api/orders/1/status
    [HttpPut("{id:int}/status")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusDto dto)
    {
        var status = NormalizeStatus(dto.Status);
        if (status == null)
            return BadRequestMessage("Trạng thái không hợp lệ! (PAID hoặc CANCELLED)");

        await using var transaction = await _db.Database.BeginTransactionAsync();

        var order = await LoadOrderWithItemsAsync(id);
        if (order == null) return NotFoundMessage("Không tìm thấy đơn hàng!");

        if (order.Status == OrderStatus.Cancelled)
            return BadRequestMessage("Đơn đã hủy, không thể đổi trạng thái!");

        if (status == OrderStatus.Cancelled)
            await RestoreStockAndPointsAsync(order);

        order.Status = status;
        await _db.SaveChangesAsync();
        await transaction.CommitAsync();

        return NoContent();
    }

    // DELETE: api/orders/1
    [HttpDelete("{id:int}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync();

        var order = await LoadOrderWithItemsAsync(id);
        if (order == null) return NotFoundMessage("Không tìm thấy đơn hàng!");

        // Đơn chưa hủy thì hoàn kho trước khi xóa
        if (order.Status != OrderStatus.Cancelled)
            await RestoreStockAndPointsAsync(order);

        _db.OrderItems.RemoveRange(order.Items);
        _db.Orders.Remove(order);
        await _db.SaveChangesAsync();
        await transaction.CommitAsync();

        return NoContent();
    }

    private Task<Order?> LoadOrderWithItemsAsync(int id) =>
        _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.OrderId == id);

    /// <summary>Hoàn kho, thu hồi điểm đã tích và trả lại điểm đã đổi của một đơn (gọi trong transaction).</summary>
    private async Task RestoreStockAndPointsAsync(Order order)
    {
        foreach (var item in order.Items)
        {
            var quantity = item.Quantity;
            await _db.Products
                .Where(p => p.ProductId == item.ProductId)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.StockQuantity, p => p.StockQuantity + quantity));
        }

        if (order.CustomerId == null) return;

        var customer = await _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == order.CustomerId);
        if (customer != null)
        {
            var remaining = Math.Max(0, customer.RewardPoints - RewardPointsFor(order.Total));
            customer.RewardPoints = remaining + RedeemedPointsFor(order);
        }
    }

    private static int RewardPointsFor(decimal total) => (int)(total / LoyaltyRules.AmountPerRewardPoint);

    /// <summary>Số điểm đã đổi suy ra từ giá trị giảm giá của đơn (mỗi điểm = 1.000đ).</summary>
    private static int RedeemedPointsFor(Order order) => (int)(order.Discount / LoyaltyRules.DiscountPerPoint);

    private static string? NormalizeStatus(string? status) => (status ?? string.Empty).Trim().ToUpperInvariant() switch
    {
        "PAID" or "ĐÃ DUYỆT" => OrderStatus.Paid,
        "CANCELLED" or "ĐÃ HỦY" => OrderStatus.Cancelled,
        _ => null
    };

    private static OrderResponseDto ToResponse(Order o) => new()
    {
        OrderId = o.OrderId,
        OrderCode = o.OrderCode,
        CustomerId = o.CustomerId ?? 0,
        OrderDate = o.CreatedAt,
        Subtotal = o.Subtotal,
        Discount = o.Discount,
        TotalAmount = o.Total,
        Status = o.Status,
        OrderDetails = o.Items.Select(i => new OrderDetailResponseDto
        {
            OrderDetailId = i.OrderItemId,
            ProductId = i.ProductId,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice
        }).ToList()
    };
}
