namespace MiniSupermarket.API.DTOs;

public class DailyReportDto
{
    public string Date { get; set; } = string.Empty;
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public string BestSeller { get; set; } = string.Empty;
    public List<RecentOrderDto> RecentOrders { get; set; } = new();
}

public class RecentOrderDto
{
    public int OrderId { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
}

/// <summary>Bốn chỉ số của Dashboard (tính cho ngày hôm nay).</summary>
public class DashboardSummaryDto
{
    public string Date { get; set; } = string.Empty;
    public decimal TodayRevenue { get; set; }
    public int TotalOrders { get; set; }
    public int NewCustomers { get; set; }
    public int LowStockProducts { get; set; }
    public int LowStockThreshold { get; set; }
}

public class DailyRevenueDto
{
    public DateTime Date { get; set; }
    public int TotalOrders { get; set; }
    public decimal Revenue { get; set; }
}

public class TopProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int TotalQuantity { get; set; }
    public decimal Revenue { get; set; }
}

public class ShiftRevenueDto
{
    public string Cashier { get; set; } = string.Empty;
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int TotalOrders { get; set; }
    public decimal Revenue { get; set; }
}
