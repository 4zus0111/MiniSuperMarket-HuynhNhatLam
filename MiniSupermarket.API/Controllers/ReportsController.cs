using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Constants;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Data.AdoNet;
using MiniSupermarket.API.DTOs;

namespace MiniSupermarket.API.Controllers;

[Authorize]
public class ReportsController : ApiControllerBase
{
    private const int RecentOrdersCount = 20;
    private const int MaxRangeDays = 366;
    private const int DefaultTopCount = 5;
    private const int MaxTopCount = 20;

    private readonly SupermarketDbContext _db;
    private readonly ReportAdoRepository _reports;

    public ReportsController(SupermarketDbContext db, ReportAdoRepository reports)
    {
        _db = db;
        _reports = reports;
    }

    // GET: api/reports/daily?date=2026-10-23 (mặc định: hôm nay)
    [HttpGet("daily")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> GetDailyReport([FromQuery] string? date)
    {
        var startDate = (DateTime.TryParse(date, out var parsed) ? parsed : DateTime.Today).Date;
        var endDate = startDate.AddDays(1);

        var orders = await _db.Orders
            .AsNoTracking()
            .Where(o => o.CreatedAt >= startDate && o.CreatedAt < endDate && o.Status == OrderStatus.Paid)
            .Include(o => o.Items).ThenInclude(i => i.Product)
            .Include(o => o.Customer)
            .ToListAsync();

        var bestSeller = orders
            .SelectMany(o => o.Items)
            .GroupBy(i => new
            {
                i.ProductId,
                ProductName = i.Product != null ? i.Product.ProductName : $"SP #{i.ProductId}"
            })
            .Select(g => new { g.Key.ProductName, TotalQuantity = g.Sum(x => x.Quantity) })
            .OrderByDescending(x => x.TotalQuantity)
            .FirstOrDefault();

        var recentOrders = orders
            .OrderByDescending(o => o.CreatedAt)
            .Take(RecentOrdersCount)
            .Select(o => new RecentOrderDto
            {
                OrderId = o.OrderId,
                OrderCode = o.OrderCode,
                CreatedAt = o.CreatedAt,
                Total = o.Total,
                Status = o.Status,
                CustomerName = o.Customer != null ? o.Customer.CustomerName : "Khách vãng lai"
            })
            .ToList();

        return Ok(new DailyReportDto
        {
            Date = startDate.ToString("yyyy-MM-dd"),
            TotalOrders = orders.Count,
            TotalRevenue = orders.Sum(o => o.Total),
            BestSeller = bestSeller != null ? $"{bestSeller.ProductName} ({bestSeller.TotalQuantity} sp)" : "Chưa có",
            RecentOrders = recentOrders
        });
    }

    // GET: api/reports/dashboard-summary
    [HttpGet("dashboard-summary")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> GetDashboardSummary()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        var paidToday = _db.Orders.AsNoTracking()
            .Where(o => o.Status == OrderStatus.Paid && o.CreatedAt >= today && o.CreatedAt < tomorrow);

        return Ok(new DashboardSummaryDto
        {
            Date = today.ToString("yyyy-MM-dd"),
            TodayRevenue = await paidToday.SumAsync(o => (decimal?)o.Total) ?? 0m,
            TotalOrders = await paidToday.CountAsync(),
            NewCustomers = await _db.Customers.CountAsync(c => c.CreatedAt >= today && c.CreatedAt < tomorrow),
            LowStockProducts = await _db.Products.CountAsync(p => p.StockQuantity <= StockRules.LowStockThreshold),
            LowStockThreshold = StockRules.LowStockThreshold
        });
    }

    // GET: api/reports/daily-revenue?from=2026-10-17&to=2026-10-23 (mặc định: 7 ngày gần nhất)
    [HttpGet("daily-revenue")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> GetDailyRevenue([FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var end = (to ?? DateTime.Today).Date;
        var start = (from ?? end.AddDays(-6)).Date;

        if (start > end)
            return BadRequestMessage("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc!");

        var dayCount = (end - start).Days + 1;
        if (dayCount > MaxRangeDays)
            return BadRequestMessage($"Khoảng thời gian tối đa là {MaxRangeDays} ngày!");

        // Stored procedure chỉ trả về những ngày có đơn; bổ sung ngày doanh thu 0 để biểu đồ liên tục.
        var byDate = (await _reports.GetDailyRevenueAsync(start, end)).ToDictionary(r => r.Date.Date);

        var result = Enumerable.Range(0, dayCount)
            .Select(offset => start.AddDays(offset))
            .Select(day => byDate.TryGetValue(day, out var row) ? row : new DailyRevenueDto { Date = day })
            .ToList();

        return Ok(result);
    }

    // GET: api/reports/top-products?count=5&from=2026-10-01&to=2026-10-23
    [HttpGet("top-products")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> GetTopProducts(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] int count = DefaultTopCount)
    {
        count = Math.Clamp(count, 1, MaxTopCount);

        var items = _db.OrderItems.AsNoTracking()
            .Where(i => i.Order!.Status == OrderStatus.Paid);

        if (from.HasValue)
        {
            var start = from.Value.Date;
            items = items.Where(i => i.Order!.CreatedAt >= start);
        }

        if (to.HasValue)
        {
            var end = to.Value.Date.AddDays(1);
            items = items.Where(i => i.Order!.CreatedAt < end);
        }

        var top = await items
            .GroupBy(i => new { i.ProductId, i.Product!.ProductName })
            .Select(g => new TopProductDto
            {
                ProductId = g.Key.ProductId,
                ProductName = g.Key.ProductName,
                TotalQuantity = g.Sum(x => x.Quantity),
                Revenue = g.Sum(x => x.LineTotal)
            })
            .OrderByDescending(x => x.TotalQuantity)
            .Take(count)
            .ToListAsync();

        return Ok(top);
    }

    // GET: api/reports/shift-revenue?from=...&to=... (doanh thu ca của thu ngân đang đăng nhập; mặc định từ 00:00 hôm nay)
    [HttpGet("shift-revenue")]
    [Authorize(Roles = AppRoles.AdminOrCashier)]
    public async Task<IActionResult> GetShiftRevenue([FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var start = from ?? DateTime.Today;
        var end = to ?? DateTime.Now;

        if (start > end)
            return BadRequestMessage("Thời gian bắt đầu ca phải trước thời gian kết thúc!");

        var username = User.Identity?.Name;
        var cashierId = await _db.Users.AsNoTracking()
            .Where(u => u.Username == username)
            .Select(u => (int?)u.UserId)
            .FirstOrDefaultAsync();

        if (cashierId == null)
            return Unauthorized(new { message = "Tài khoản không hợp lệ!" });

        var (totalOrders, revenue) = await _reports.GetShiftRevenueAsync(cashierId.Value, start, end);

        return Ok(new ShiftRevenueDto
        {
            Cashier = username ?? string.Empty,
            From = start,
            To = end,
            TotalOrders = totalOrders,
            Revenue = revenue
        });
    }
}
