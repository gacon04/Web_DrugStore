using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Web_DrugStore.Identity;

namespace Web_DrugStore.Models
{
    public class GioHang
    {
        [Key]
        public int GioHangId { get; set; } // Mã giỏ hàng

        [Required]
        public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo giỏ hàng

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

      
        public double TongTien { get; set; } // Tổng tiền giỏ hàng

        // Quan hệ một-nhiều với ChiTietGioHang
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }
    }
}
