// ViewModels/DonHangDetailsViewModel.cs
using System;
using System.Collections.Generic;
using Web_DrugStore.Models;

namespace Web_DrugStore.ViewModels
{
    public class DonHangDetailsViewModel
    {
        public string MaDonHang { get; set; }
        public DateTime NgayDat { get; set; }
        public TrangThaiDonHang TrangThai { get; set; }
        public double TongTienHang { get; set; }
        public double VAT { get; set; }
        public double PhiVanChuyen { get; set; }
        public double TongHoaDon { get; set; }
        public string TenKhachHang { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public string SoDienThoai { get; set; }
        public List<DonHangChiTietViewModel> DonHangChiTiet { get; set; }
    }

    public class DonHangChiTietViewModel
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien => SoLuong * DonGia;
    }
}