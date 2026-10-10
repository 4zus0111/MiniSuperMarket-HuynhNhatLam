using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    public class CheckoutItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class CheckoutRequest
    {
        public string? CashierUsername { get; set; }
        public string? CustomerPhone { get; set; }
        public decimal CashReceived { get; set; }
        public List<CheckoutItem> Items { get; set; } = new();
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly SupermarketDbContext _db;
        public OrdersController(SupermarketDbContext db) => _db = db;

        private static object Map(Order o) => new
        {
            o.OrderId,
            o.OrderCode,
            CustomerId = o.CustomerId ?? 0,
            OrderDate = o.CreatedAt,
            TotalAmount = o.Total,
            o.Status,
            OrderDetails = o.Items.Select(i => new
            {
                OrderDetailId = i.OrderItemId,
                i.ProductId,
                i.Quantity,
                i.UnitPrice
            })
        };

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Orders.Include(o => o.Items)
                .OrderByDescending(o => o.CreatedAt).AsNoTracking().ToListAsync();
            return Ok(list.Select(Map));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var o = await _db.Orders.Include(x => x.Items).AsNoTracking().FirstOrDefaultAsync(x => x.OrderId == id);
            return o == null ? NotFound(new { message = "Không tìm thấy đơn hàng!" }) : Ok(Map(o));
        }

        // POS thanh toán
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest req)
        {
            if (req.Items == null || req.Items.Count == 0)
                return BadRequest(new { message = "Giỏ hàng trống!" });

            var cashier = await _db.Users.FirstOrDefaultAsync(u => u.Username == req.CashierUsername);
            // Tài khoản đăng nhập mẫu (admin/cashier) chưa có trong bảng Users -> dùng thu ngân đầu tiên trong DB
            cashier ??= await _db.Users.Include(u => u.Role)
                .Where(u => u.Role != null && u.Role.RoleName == "CASHIER")
                .OrderBy(u => u.UserId).FirstOrDefaultAsync()
                ?? await _db.Users.OrderBy(u => u.UserId).FirstOrDefaultAsync();
            if (cashier == null)
                return BadRequest(new { message = "Chưa có tài khoản nào trong bảng Users!" });

            Customer? customer = null;
            if (!string.IsNullOrWhiteSpace(req.CustomerPhone))
                customer = await _db.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == req.CustomerPhone.Trim());

            var ids = req.Items.Select(i => i.ProductId).Distinct().ToList();
            var products = await _db.Products.Where(p => ids.Contains(p.ProductId)).ToDictionaryAsync(p => p.ProductId);

            var order = new Order
            {
                CashierId = cashier.UserId,
                CustomerId = customer?.CustomerId,
                PaymentMethod = "CASH",
                Status = "PAID",
                CreatedAt = DateTime.Now,
                OrderCode = "TMP"
            };

            foreach (var line in req.Items)
            {
                if (line.Quantity <= 0) return BadRequest(new { message = "Số lượng không hợp lệ!" });
                if (!products.TryGetValue(line.ProductId, out var p))
                    return BadRequest(new { message = $"Sản phẩm #{line.ProductId} không tồn tại!" });
                if (p.StockQuantity < line.Quantity)
                    return BadRequest(new { message = $"'{p.ProductName}' chỉ còn {p.StockQuantity} trong kho!" });

                p.StockQuantity -= line.Quantity;
                order.Items.Add(new OrderItem
                {
                    ProductId = p.ProductId,
                    Quantity = line.Quantity,
                    UnitPrice = p.Price,
                    LineTotal = p.Price * line.Quantity
                });
            }

            order.Subtotal = order.Items.Sum(i => i.LineTotal);
            order.Discount = 0;
            order.Total = order.Subtotal;

            if (req.CashReceived < order.Total)
                return BadRequest(new { message = "Tiền khách đưa chưa đủ!" });

            if (customer != null)
                customer.RewardPoints += (int)(order.Total / 10000m);

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            order.OrderCode = $"HD{order.OrderId:D5}";
            await _db.SaveChangesAsync();

            return Ok(new { order.OrderId, order.OrderCode, order.Total, Change = req.CashReceived - order.Total });
        }

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null) return NotFound(new { message = "Không tìm thấy đơn hàng!" });
            if (string.IsNullOrWhiteSpace(status)) return BadRequest(new { message = "Trạng thái không hợp lệ!" });
            order.Status = status;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null) return NotFound(new { message = "Không tìm thấy đơn hàng!" });
            _db.OrderItems.RemoveRange(order.Items);
            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}