using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_DrugStore.Models
{
    public class DanhMuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string TenDanhMuc { get; set; }

        [StringLength(1000)]
        public string MoTa { get; set; }
        
        [Required]        
        public bool HoatDong { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual DanhMuc DanhMucCha { get; set; }

        public virtual ICollection<DanhMuc> DanhMucCon { get; set; }

        // Liên kết với bảng SanPham
        public virtual ICollection<SanPham> SanPhams { get; set; } // Một danh mục có nhiều sản phẩm
    }
}