using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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

        [Required]
        public decimal DonGia { get; set; }

        public int SoLuong { get; set; }

        public string HinhAnh { get; set; }

        // Phân loại sản phẩm
        [Required]
        public string PhanLoai { get; set; }

        public Boolean HoatDong { get; set; }
        // Danh mục sản phẩm
        [Required]
        [ForeignKey("DanhMuc")]
        
        public int DanhMucId { get; set; }
        public virtual DanhMuc DanhMuc { get; set; }

        // Công dụng sản phẩm
        public string CongDung { get; set; }

        // Quy cách sản phẩm
        public string QuyCach { get; set; }

        // Lưu ý khi sử dụng
        public string LuuY { get; set; }

        // Nhà sản xuất
        [Required]
        public string NhaSanXuat { get; set; }

        // Điều hướng quan hệ - Một sản phẩm có nhiều hình ảnh
        public virtual ICollection<HinhAnhSanPham> HinhAnhSanPhams { get; set; }
    }
}