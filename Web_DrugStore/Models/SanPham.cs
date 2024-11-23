using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_DrugStore.Models
{
    public class SanPham
    {
        [Key]
        public int SanPhamId { get; set; }

        [Required]
        [StringLength(255)]
        public string TenSanPham { get; set; }

        public string MoTa { get; set; }

        public decimal DonGiaGoc { get; set; }

        [Required]
        public decimal DonGia { get; set; }

        
        public int SoLuong { get; set; }


        [Required]
        public string PhanLoai { get; set; }

        public bool HoatDong { get; set; }

        [Required]
        [ForeignKey("DanhMuc")]
        public int DanhMucId { get; set; }
        public virtual DanhMuc DanhMuc { get; set; }

        public string CongDung { get; set; }


        public string QuyCach { get; set; }

        public string LuuY { get; set; }

        [Required]
        public string NhaSanXuat { get; set; }

        public string Thumbnail { get; set; }

        public virtual ICollection<HinhAnhSanPham> HinhAnhSanPhams { get; set; }

        public int LuotYeuThich { get; set; } = 0;

        public int LuotMua { get; set; } = 0;

        public bool Hot { get; set; } = false;

        public decimal Rated { get; set; } = 0;
        public DateTime NgayTao { get; set; } = DateTime.Now;

        public DateTime? NgayCapNhat { get; set; }

    }
}
