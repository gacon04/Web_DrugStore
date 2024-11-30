using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public DateTime? NgayGiao { get; set; }

        public string CachThanhToan { get; set; }

        public string CachVanChuyen { get; set; }


        [Required]
        [StringLength(255)]
        public string TenKhachHang { get; set; } // Tên khách hàng

        [Required]
        [StringLength(255)]
        public string DiaChi { get; set; } // Địa chỉ giao hàng

        [StringLength(255)]
        public string DiaChiChoLam { get; set; }

        [Required]
        public string UserAspId { get; set; }

        [Required]
        [StringLength(20)]
        public string SoDienThoai { get; set; } // Số điện thoại khách hàng

        [StringLength(255)]
        public string Email { get; set; } // Email khách hàng

        public double TongTienHang{ get; set; } // Tổng tiền đơn hàng
  
        public double TongHoaDon { get; set; }
        

        public double VAT { get; set; }

        public double GiamGia { get; set; } = 0;

        public string CodeGiamGia { get; set; } = "";

        public double PhiVanChuyen { get; set; } = 20000;

        public string GhiChu { get; set; } = "";

        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } // Liên kết với bảng ChiTietDonHang
    }
}