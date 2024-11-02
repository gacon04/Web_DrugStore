using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Web_DrugStore.Models;

namespace Web_DrugStore.Models
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(255, ErrorMessage = "Tiêu đề không được vượt quá 255 ký tự")]
        public string TieuDe { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống")]
        [Column(TypeName = "nvarchar(MAX)")]
        public string NoiDung { get; set; }

        [ForeignKey("TacGia")]
        public int IdTacGia { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool HienThi { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [StringLength(255, ErrorMessage = "Đường dẫn hình ảnh không được vượt quá 255 ký tự")]
        public string DuongDanHinhAnh { get; set; }

        // Foreign key và điều hướng tới DanhMucBlog
        [ForeignKey("DanhMucBlog")]
        public int DanhMucBlogId { get; set; }

        public virtual DanhMucBlog DanhMucBlog { get; set; } // Điều hướng quan hệ

        // Điều hướng quan hệ tới tác giả
        public virtual TaiKhoan TacGia { get; set; }
    }
}
