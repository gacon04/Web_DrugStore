using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_DrugStore.ViewModel
{
    public class AccountDetailVM
    {
        public string Id { get; set; } // User ID

        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Họ và Tên")]
        public string HoTen { get; set; }

        [Display(Name = "Số điện thoại")]
        [Phone]
        public string SDT { get; set; }
        [Display(Name = "Địa chỉ đăng kí")]
        public string DiaChi { get; set; }

    }
}