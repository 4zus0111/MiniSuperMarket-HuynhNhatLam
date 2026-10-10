using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    public class Customer
    {
        // Khóa chính - tự tăng IDENTITY(1,1)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        // Tên khách hàng - bắt buộc, NVARCHAR(100)
        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        // Số điện thoại - bắt buộc, VARCHAR(15)
        [Required]
        [MaxLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string PhoneNumber { get; set; } = string.Empty;

        // Địa chỉ - có thể để trống, NVARCHAR(200)
        [MaxLength(200)]
        public string? Address { get; set; }

        // Điểm tích lũy - mặc định 0
        public int RewardPoints { get; set; } = 0;

        // Hạng thành viên - mặc định "Chuẩn"
        [MaxLength(50)]
        public string MembershipRank { get; set; } = "Chuẩn";
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
