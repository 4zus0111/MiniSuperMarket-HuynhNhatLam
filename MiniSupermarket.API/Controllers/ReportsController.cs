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
