using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    [Table("Brands")]
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Tên thương hiệu không được để trống")]
        [StringLength(100)]
        public string BrandName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(200)]
        public string? LogoUrl { get; set; } // Link ảnh logo thương hiệu (tùy chọn)

        // Quan hệ 1-N: 1 Thương hiệu có thể có nhiều Sản phẩm
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}