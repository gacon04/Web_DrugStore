namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Content = c.String(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ImagePath = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChiTietDonHangs",
                c => new
                    {
                        ChiTietDonHangId = c.Int(nullable: false, identity: true),
                        SoLuong = c.Int(nullable: false),
                        DonGia = c.Double(nullable: false),
                        DonHangId = c.Int(nullable: false),
                        SanPhamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChiTietDonHangId)
                .ForeignKey("dbo.DonHangs", t => t.DonHangId, cascadeDelete: true)
                .ForeignKey("dbo.SanPhams", t => t.SanPhamId, cascadeDelete: true)
                .Index(t => t.DonHangId)
                .Index(t => t.SanPhamId);
            
            CreateTable(
                "dbo.DonHangs",
                c => new
                    {
                        DonHangId = c.Int(nullable: false, identity: true),
                        NgayDat = c.DateTime(nullable: false),
                        TenKhachHang = c.String(nullable: false, maxLength: 255),
                        DiaChi = c.String(nullable: false, maxLength: 255),
                        SoDienThoai = c.String(nullable: false, maxLength: 20),
                        Email = c.String(maxLength: 255),
                        TongTien = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DonHangId);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        SanPhamId = c.Int(nullable: false, identity: true),
                        TenSanPham = c.String(nullable: false),
                        MoTa = c.String(),
                        DonGia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SoLuong = c.Int(nullable: false),
                        HinhAnh = c.String(),
                        DanhMuc_Id = c.Int(),
                    })
                .PrimaryKey(t => t.SanPhamId)
                .ForeignKey("dbo.DanhMucs", t => t.DanhMuc_Id)
                .Index(t => t.DanhMuc_Id);
            
            CreateTable(
                "dbo.ChiTietGioHangs",
                c => new
                    {
                        ChiTietGioHangId = c.Int(nullable: false, identity: true),
                        GioHangId = c.Int(nullable: false),
                        SanPhamId = c.Int(nullable: false),
                        SoLuong = c.Int(nullable: false),
                        DonGia = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ChiTietGioHangId)
                .ForeignKey("dbo.GioHangs", t => t.GioHangId, cascadeDelete: true)
                .ForeignKey("dbo.SanPhams", t => t.SanPhamId, cascadeDelete: true)
                .Index(t => t.GioHangId)
                .Index(t => t.SanPhamId);
            
            CreateTable(
                "dbo.GioHangs",
                c => new
                    {
                        GioHangId = c.Int(nullable: false, identity: true),
                        NgayTao = c.DateTime(nullable: false),
                        TenKhachHang = c.String(nullable: false, maxLength: 255),
                        DiaChi = c.String(nullable: false, maxLength: 255),
                        SoDienThoai = c.String(nullable: false, maxLength: 20),
                        Email = c.String(maxLength: 255),
                        TongTien = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.GioHangId);
            
            CreateTable(
                "dbo.DanhMucs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenDanhMuc = c.String(nullable: false, maxLength: 255),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DanhMucs", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.TaiKhoans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DiaChi = c.String(maxLength: 255),
                        Email = c.String(nullable: false),
                        SDT = c.String(nullable: false),
                        TenNguoiDung = c.String(nullable: false, maxLength: 50),
                        MatKhau = c.String(nullable: false, maxLength: 100),
                        VaiTro = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TinNhanLienHes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HoTen = c.String(nullable: false, maxLength: 100),
                        SDT = c.String(nullable: false, maxLength: 15),
                        Email = c.String(maxLength: 100),
                        NoiDung = c.String(nullable: false),
                        ThoiGian = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SanPhams", "DanhMuc_Id", "dbo.DanhMucs");
            DropForeignKey("dbo.DanhMucs", "ParentId", "dbo.DanhMucs");
            DropForeignKey("dbo.ChiTietGioHangs", "SanPhamId", "dbo.SanPhams");
            DropForeignKey("dbo.ChiTietGioHangs", "GioHangId", "dbo.GioHangs");
            DropForeignKey("dbo.ChiTietDonHangs", "SanPhamId", "dbo.SanPhams");
            DropForeignKey("dbo.ChiTietDonHangs", "DonHangId", "dbo.DonHangs");
            DropIndex("dbo.DanhMucs", new[] { "ParentId" });
            DropIndex("dbo.ChiTietGioHangs", new[] { "SanPhamId" });
            DropIndex("dbo.ChiTietGioHangs", new[] { "GioHangId" });
            DropIndex("dbo.SanPhams", new[] { "DanhMuc_Id" });
            DropIndex("dbo.ChiTietDonHangs", new[] { "SanPhamId" });
            DropIndex("dbo.ChiTietDonHangs", new[] { "DonHangId" });
            DropTable("dbo.TinNhanLienHes");
            DropTable("dbo.TaiKhoans");
            DropTable("dbo.DanhMucs");
            DropTable("dbo.GioHangs");
            DropTable("dbo.ChiTietGioHangs");
            DropTable("dbo.SanPhams");
            DropTable("dbo.DonHangs");
            DropTable("dbo.ChiTietDonHangs");
            DropTable("dbo.Blogs");
        }
    }
}
