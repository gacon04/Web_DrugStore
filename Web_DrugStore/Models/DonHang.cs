using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_DrugStore.Models
{
    public class DonHang
    {
        [Key]
        public int DonHangId { get; set; } // Mã đơn hàng

        [Required]
        public DateTime NgayDat { get; set; } = DateTime.Now; // Ngày đặt hàng

        [Required]
        [StringLength(255)]
        public string TenKhachHang { get; set; } // Tên khách hàng

        [Required]
        [StringLength(255)]
        public string DiaChi { get; set; } // Địa chỉ giao hàng

        [Required]
        [StringLength(20)]
        public string SoDienThoai { get; set; } // Số điện thoại khách hàng

        [StringLength(255)]
        public string Email { get; set; } // Email khách hàng

        public double TongTien { get; set; } // Tổng tiền đơn hàng

        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } // Liên kết với bảng ChiTietDonHang
    }
}