namespace MiniSupermarket.API.DTOs;

public class CheckoutItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public class CheckoutRequest
{
    public string? CustomerPhone { get; set; }
    public decimal CashReceived { get; set; }
    public List<CheckoutItem> Items { get; set; } = new();
}

public class UpdateOrderStatusDto
{
    public string Status { get; set; } = string.Empty;
}
