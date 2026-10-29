using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.DTOs;

public class CustomerRequest
{
    [Required(ErrorMessage = "Tên khách hàng không được để trống!")]
    [StringLength(100, ErrorMessage = "Tên khách hàng không vượt quá 100 ký tự")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Số điện thoại không được để trống!")]
    [StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 15 ký tự")]
    public string PhoneNumber { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "Địa chỉ không vượt quá 200 ký tự")]
    public string? Address { get; set; }

    /// <summary>Chỉ dùng khi cập nhật; khách mới luôn bắt đầu với 0 điểm.</summary>
    [Range(0, int.MaxValue, ErrorMessage = "Điểm tích lũy không được âm")]
    public int RewardPoints { get; set; }

    /// <summary>Chỉ dùng khi cập nhật; khách mới luôn có hạng "Chuẩn".</summary>
    [StringLength(50, ErrorMessage = "Hạng thành viên không vượt quá 50 ký tự")]
    public string MembershipRank { get; set; } = "Chuẩn";
}

public class CustomerDto
{
    public static readonly Expression<Func<Customer, CustomerDto>> Projection = c => new CustomerDto
    {
        CustomerId = c.CustomerId,
        CustomerName = c.CustomerName,
        PhoneNumber = c.PhoneNumber,
        Address = c.Address,
        RewardPoints = c.RewardPoints,
        MembershipRank = c.MembershipRank,
        CreatedAt = c.CreatedAt
    };

    private static readonly Func<Customer, CustomerDto> Compiled = Projection.Compile();

    public static CustomerDto From(Customer customer) => Compiled(customer);

    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Address { get; set; }
    public int RewardPoints { get; set; }
    public string MembershipRank { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
