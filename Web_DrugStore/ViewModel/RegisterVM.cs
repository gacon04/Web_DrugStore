using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Web_DrugStore.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Vui lòng không để trống họ tên")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống số điện thoại")]
        [RegularExpression(@"^\d{5,15}$", ErrorMessage = "Gãy nhập vào số điện thoại hợp lệ")]
        public string SDT { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&\.])[A-Za-z\d@$!%*?&\.]{6,}$",
    ErrorMessage = "Mật khẩu phải từ 6 ký tự trở lên, có ít nhất 1 chữ cái hoa, 1 chữ cái thường, 1 ký tự đặc biệt và 1 chữ số")]

        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [Compare("MatKhau", ErrorMessage ="Nhập lại mật khẩu không trùng khớp, vui lòng kiểm tra lại")]
        public string NhapLaiMatKhau { get; set; }
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống Email")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email nhập vào không hợp lệ")]
        public string Email { get; set; }

    }
}