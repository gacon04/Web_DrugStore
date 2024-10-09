using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_DrugStore.Models
{
    public class DanhSachYeuThich
    {
        [Key]
        public int WishlistId { get; set; }

        [Required]
        public int TaiKhoanId { get; set; } // Khóa ngoại liên kết với tài khoản

        [Required]
        public int SanPhamId { get; set; } // Khóa ngoại liên kết với sản phẩm

        public DateTime NgayThem { get; set; } = DateTime.Now; // Ngày thêm vào danh sách yêu thích

        // Điều hướng tới bảng TaiKhoan và SanPham
        [ForeignKey("TaiKhoanId")]
        public virtual TaiKhoan TaiKhoan { get; set; }

        [ForeignKey("SanPhamId")]
        public virtual SanPham SanPham { get; set; }
    }
}