using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_DrugStore.Models
{
    public class TaiKhoan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [Required]
        [EmailAddress] // Kiểm tra định dạng email
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string HoTen { get; set; }
        [Required]
        [Phone]
        public string SDT { get; set; }

        [Required]
        [StringLength(50)]
        public string TenNguoiDung { get; set; }

        [Required]
        [StringLength(100)]
        public string MatKhau { get; set; }

        [Required]
        [StringLength(50)]
        public string VaiTro { get; set; }
    }
}