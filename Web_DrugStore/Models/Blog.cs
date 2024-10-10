using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "nvarchar(MAX)")] // Sử dụng nvarchar(MAX) để lưu nội dung lớn
        public string NoiDung { get; set; }

        [ForeignKey("TacGia")]
        public int IdTacGia { get; set; } // Liên kết với bảng Users (hoặc Admin)

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Đường dẫn hình ảnh
        [StringLength(255, ErrorMessage = "Đường dẫn hình ảnh không được vượt quá 255 ký tự")]
        public string DuongDanHinhAnh { get; set; } // Lưu đường dẫn ảnh

        // Điều hướng quan hệ
        public virtual TaiKhoan TacGia { get; set; } // Thay đổi tùy theo class Author
    }
}
