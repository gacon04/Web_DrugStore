using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web_DrugStore.ViewModel
{
    public class RegisterVM
    {
        private string _hoTen;

        [Required(ErrorMessage = "Vui lòng không để trống họ tên")]
        [RegularExpression(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơẠ-ỹ]{2,}\s+[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơẠ-ỹ]+$",
    ErrorMessage = "Vui lòng nhập họ tên hợp lệ.")]
        [StringLength(50, ErrorMessage = "Họ tên không được vượt quá 50 ký tự.")]
        public string HoTen
        {
            get => _hoTen;
            set => _hoTen = string.IsNullOrWhiteSpace(value)
                ? null
                : Regex.Replace(value.Trim(), @"\s+", " "); // Loại bỏ khoảng trắng thừa
        }


        [Required(ErrorMessage = "Vui lòng không để trống số điện thoại")]
        [RegularExpression(@"^(032|033|034|035|036|037|038|039|081|082|083|084|085|070|076|077|078|079|056|058|059)\d{7}$",
            ErrorMessage = "Số điện thoại không hợp lệ. Vui lòng nhập số điện thoại cá nhân Việt Nam.")]
        public string SDT { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&\.])[A-Za-z\d@$!%*?&\.]{6,}$",
            ErrorMessage = "Mật khẩu phải từ 6 ký tự trở lên, có ít nhất 1 chữ cái hoa, 1 chữ cái thường, 1 ký tự đặc biệt và 1 chữ số.")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [Compare("MatKhau", ErrorMessage = "Nhập lại mật khẩu không trùng khớp, vui lòng kiểm tra lại.")]
        public string NhapLaiMatKhau { get; set; }

        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống Email")]
        [EmailAddress(ErrorMessage = "Email nhập vào không hợp lệ")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = "Vui lòng nhập vào email hợp lệ.")]
        public string Email { get; set; }
    }
}
