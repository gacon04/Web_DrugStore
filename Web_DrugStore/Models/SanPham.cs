using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_DrugStore.Models
{
    public class SanPham
    {
        [Key]
        public int SanPhamId { get; set; }

        [Required]
        public string TenSanPham { get; set; }

        public string MoTa { get; set; }

        [Required]
        public decimal DonGia { get; set; }

        public int SoLuong { get; set; }

        public string HinhAnh { get; set; }
    }
}