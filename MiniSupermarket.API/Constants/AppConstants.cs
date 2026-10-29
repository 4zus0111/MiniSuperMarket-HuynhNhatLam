namespace MiniSupermarket.API.Constants;

public static class AppRoles
{
    public const string Admin = "Admin";
    public const string Cashier = "Cashier";
    public const string Warehouse = "Warehouse";

    public const string AdminOrCashier = Admin + "," + Cashier;
    public const string AdminOrWarehouse = Admin + "," + Warehouse;

    /// <summary>Chuẩn hóa tên vai trò lưu trong DB (ADMIN, CASHIER...) về dạng dùng trong JWT.</summary>
    public static string Normalize(string? roleName) => (roleName ?? string.Empty).ToUpperInvariant() switch
    {
        "ADMIN" => Admin,
        "CASHIER" => Cashier,
        "WAREHOUSE" => Warehouse,
        _ => roleName ?? string.Empty
    };
}

public static class OrderStatus
{
    public const string Paid = "PAID";
    public const string Cancelled = "CANCELLED";
}

public static class LoyaltyRules
{
    /// <summary>Mỗi 10.000đ thanh toán được 1 điểm.</summary>
    public const decimal AmountPerRewardPoint = 10_000m;

    /// <summary>Đổi điểm theo từng bước: 50 điểm = giảm 50.000đ.</summary>
    public const int PointsPerRedemption = 50;
    public const decimal DiscountPerRedemption = 50_000m;
    public const decimal DiscountPerPoint = DiscountPerRedemption / PointsPerRedemption;
}

public static class StockRules
{
    /// <summary>Sản phẩm có tồn kho từ ngưỡng này trở xuống bị coi là sắp hết hàng.</summary>
    public const int LowStockThreshold = 10;
}
