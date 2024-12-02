using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_DrugStore.ViewModel
{
    public class CheckOutItemVM
    {
        [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
        [StringLength(255)]
        public string TenKhachHang { get; set; } // Tên khách hàng

        [StringLength(255)]
        public string Email { get; set; } // Email khách hàng

        [Required]
        [StringLength(15)]
        public string SoDienThoai { get; set; } // Số điện thoại khách hàng

        [StringLength(255)]
        public string DiaChiChoLam { get; set; }

        public string SoNha { get; set; } = "";

        public string TenDuong { get; set; } = "";

        public string TenXa { get; set; }
        public string GhiChu { get; set; } = "";
        public string TenHuyen { get; set; }
        public string TenTinh { get; set; }

        public string PhuongThucThanhToan { get; set; }
    }
}