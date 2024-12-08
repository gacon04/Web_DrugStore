using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_DrugStore.Models
{
    public class DanhSachYeuThich
    {
        [Key]
        public int WishlistId { get; set; }

        [Required]
        public string UserAspId { get; set; } // Khóa ngoại liên kết với tài khoản

        public virtual ICollection<SanPham> DanhSachSanPham { get; set; } // Liên kết với bảng ChiTietDonHang
        public DanhSachYeuThich()
        {
            DanhSachSanPham = new List<SanPham>();
        }
    }
}