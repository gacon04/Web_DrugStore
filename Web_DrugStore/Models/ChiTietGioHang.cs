using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_DrugStore.Models
{
    public class ChiTietGioHang
    {
        [Key]
        public int ChiTietGioHangId { get; set; } // Mã chi tiết giỏ hàng

        [Required]
        public int GioHangId { get; set; } // Mã giỏ hàng (khóa ngoại)
        public virtual GioHang GioHang { get; set; } // Điều hướng đến bảng GioHang

        [Required]
        public int SanPhamId { get; set; } // Mã sản phẩm (khóa ngoại)
        public virtual SanPham SanPham { get; set; } // Điều hướng đến bảng SanPham

        [Required]
        public int SoLuong { get; set; } // Số lượng sản phẩm

        [Required]
        public double DonGia { get; set; } // Đơn giá sản phẩm

        public double ThanhTien => SoLuong * DonGia; // Thành tiền
    }
}
