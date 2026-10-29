using System.ComponentModel.DataAnnotations;

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

    /// <summary>Số điểm thưởng muốn đổi (bội số của 50; 50 điểm = giảm 50.000đ).</summary>
    public int RedeemPoints { get; set; }

    public decimal CashReceived { get; set; }
    public List<CheckoutItem> Items { get; set; } = new();
}

public class CheckoutResponseDto
{
    public int OrderId { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public decimal Change { get; set; }
    public int RewardPointsRedeemed { get; set; }
    public int RewardPointsEarned { get; set; }
}

public class UpdateOrderStatusDto
{
    [Required(ErrorMessage = "Vui lòng chọn trạng thái!")]
    public string Status { get; set; } = string.Empty;
}

public class OrderResponseDto
{
    public int OrderId { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<OrderDetailResponseDto> OrderDetails { get; set; } = new();
}

public class OrderDetailResponseDto
{
    public int OrderDetailId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
