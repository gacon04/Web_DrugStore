using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_DrugStore.Models
{
    public class DanhMucBlog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100, ErrorMessage = "Tên danh mục không được vượt quá 100 ký tự")]
        public string TenDanhMuc { get; set; }

        [StringLength(255, ErrorMessage = "Mô tả không được vượt quá 255 ký tự")]
        public string MoTa { get; set; }
        public bool HoatDong { get; set; }

        // Điều hướng quan hệ
        public virtual ICollection<Blog> Blogs { get; set; } // Liên kết nhiều bài viết với một danh mục
    }
}
