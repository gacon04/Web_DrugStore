using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_DrugStore.Models
{
    public class ChiTietDonHang
    {
        [Key]
        public int ChiTietDonHangId { get; set; } // Mã chi tiết đơn hàng

        [Required]
        public int SoLuong { get; set; } // Số lượng sản phẩm

        [Required]
        public double DonGia { get; set; } // Đơn giá của sản phẩm tại thời điểm mua

        // Liên kết với bảng DonHang
        [ForeignKey("DonHang")]
        public int DonHangId { get; set; } // Mã đơn hàng

        public virtual DonHang DonHang { get; set; } // Navigation property tới bảng DonHang

        // Liên kết với bảng SanPham
        [ForeignKey("SanPham")]
        public int SanPhamId { get; set; } // Mã sản phẩm

        public virtual SanPham SanPham { get; set; } // Navigation property tới bảng SanPham
    }
}