using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using IdGen;
using Web_DrugStore.FuncService;
namespace Web_DrugStore.Models
{
    public enum TrangThaiDonHang
    {
        [Display(Name = "Chờ xác nhận")]
        ChoXacNhan = 0,

        [Display(Name = "Đang xử lý")]
        DangXuLy = 1,

        [Display(Name = "Đang giao hàng")]
        DangGiao = 2,

        [Display(Name = "Đã giao")]
        DaGiao = 3,

        [Display(Name = "Đã hủy")]
        DaHuy = 4,   

   
    }
    public enum HinhThucThanhToan
    {
        [Display(Name = "Trả tiền khi nhận hàng (COD)")]
        COD = 0,
        [Display(Name = "Thanh toán qua VNPAY")]
        VNPAY = 1,

        [Display(Name = "Thanh toán qua Momo")]
        MOMO = 2

    }
    public class DonHang
    {
        [Key]
        public int DonHangId { get; set; } // Mã đơn hàng

        [Required]
        public string UserAspId { get; set; }

        public string MaDonHang { get; set; }
        public void GenerateMaDonHang()
        {
            // Sử dụng IdGeneratorService để sinh mã đơn hàng
            long id = IdGeneratorService.GenerateId();
            MaDonHang = $"DH{id}"; // Gán định dạng
        }
        [Required]
        [StringLength(255)]
        public string TenKhachHang { get; set; } // Tên khách hàng

        [StringLength(255)]
        public string Email { get; set; } // Email khách hàng

        [Required]
        [StringLength(15)]
        public string SoDienThoai { get; set; } // Số điện thoại khách hàng

        [StringLength(255)]
        public string DiaChiChoLam { get; set; }
        public string SoNha { get; set; } = "";

        public string TenDuong { get; set; } = "";

        public string TenXa { get; set; }

        public string TenHuyen { get; set; }
        public string TenTinh { get; set; }


        [Required]
        public DateTime NgayDat { get; set; } = DateTime.Now; // Ngày đặt hàng

        public DateTime? NgayGiao { get; set; }

        public HinhThucThanhToan CachThanhToan { get; set; }

        public string GhiChu { get; set; } = "";

        [Required]
        public TrangThaiDonHang TrangThai { get; set; } = 0;

        public double TongTienHang { get; set; } // Tổng tiền đơn hàng
        public double VAT { get; set; }
        public double PhiVanChuyen { get; set; } = 20000;

        public double TongHoaDon { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } // Liên kết với bảng ChiTietDonHang






    }
}
