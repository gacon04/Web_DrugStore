using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_DrugStore.Models
{
    public class HinhAnhSanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("SanPham")]
        public int SanPhamId { get; set; } // Khóa ngoại tham chiếu đến bảng SanPham

        [Required]
        [StringLength(255)]
        public string DuongDan { get; set; } // Đường dẫn hình ảnh bổ sung của thuốc

        // Điều hướng quan hệ
        public virtual SanPham SanPham { get; set; } // Liên kết với bảng SanPham
    }
}