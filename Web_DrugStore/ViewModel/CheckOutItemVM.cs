using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web_DrugStore.ViewModel
{
    public class CheckOutItemVM
    {
        private string _hoTen;

        [Required(ErrorMessage = "Vui lòng không để trống họ tên")]
        [RegularExpression(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơẠ-ỹ]{2,}(\s+[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơẠ-ỹ]+)+$",
        ErrorMessage = "Vui lòng nhập họ tên hợp lệ và chú ý khoảng trắng.")]
        [StringLength(50, ErrorMessage = "Họ tên không được vượt quá 50 ký tự.")]
        public string TenKhachHang
        {
            get => _hoTen;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    // Chuẩn hóa chuỗi: Loại bỏ khoảng trắng thừa
                    var trimmedValue = Regex.Replace(value.Trim(), @"\s{2,}", " ");
                    _hoTen = trimmedValue.Length > 50
                        ? trimmedValue.Substring(0, 50) // Đảm bảo không vượt quá 50 ký tự
                        : trimmedValue;
                }
                else
                {
                    _hoTen = null; // Nếu chuỗi rỗng hoặc chỉ chứa khoảng trắng
                }
            }
        }

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
