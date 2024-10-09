using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_DrugStore.Models
{
    public class TinNhanLienHe
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        [StringLength(100, ErrorMessage = "Họ và tên không được vượt quá 100 ký tự")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự")]
        public string SDT { get; set; }

        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nội dung tin nhắn là bắt buộc")]
        public string NoiDung { get; set; }

        public DateTime ThoiGian { get; set; } = DateTime.Now;
    }
}