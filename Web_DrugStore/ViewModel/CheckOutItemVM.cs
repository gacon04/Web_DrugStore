using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web_DrugStore.ViewModel
{
    public class CheckOutItemVM
    {
        [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Vui lòng nhập tên hợp lệ")]
        [RegularExpression(@"^[a-zA-ZÀ-ỹ\s]+$", ErrorMessage = "Tên khách hàng chỉ được chứa các ký tự chữ cái.")]
        public string TenKhachHang { get; set; } // Tên khách hàng

        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email.")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập địa chỉ email hợp lệ.")]
        public string Email { get; set; } // Email khách hàng

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Vui lòng nhập vào số điện thoại hợp lệ.")]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Vui lòng nhập vào số điện thoại hợp lệ.")]
        public string SoDienThoai { get; set; } // Số điện thoại khách hàng

        [StringLength(255)]
        public string DiaChiChoLam { get; set; }

        public string SoNha { get; set; } = "";

        public string TenDuong { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập tên xã/phường.")]
        public string TenXa { get; set; }

        public string GhiChu { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập tên quận/huyện.")]
        public string TenHuyen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên tỉnh.")]
        public string TenTinh { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán.")]
        public string PhuongThucThanhToan { get; set; }

       
    }
}
