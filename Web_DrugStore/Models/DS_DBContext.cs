using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Web_DrugStore.Models
{
    public class DS_DBContext:DbContext
    {
        public DS_DBContext() : base("DSConnectionString") { }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<TinNhanLienHe> TinNhanLienHes { get; set; }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public DbSet<DanhSachYeuThich> DanhSachYeuThichs { get; set; }
        public DbSet<DanhMucBlog> DanhMucBlogs { get; set; }
        public DbSet<HinhAnhSanPham> HinhAnhSanPhams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DanhSachYeuThich>()
                .HasMany(w => w.DanhSachSanPham)
                .WithMany(p => p.DanhSachYeuThichs)
                .Map(m =>
                {
                    m.ToTable("WishlistSanPham");
                    m.MapLeftKey("WishlistId");
                    m.MapRightKey("SanPhamId");
                });
        }



    }
}