namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateHinhThucThanhToan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHangs", "SoNha", c => c.String());
            AddColumn("dbo.DonHangs", "TenDuong", c => c.String());
            AddColumn("dbo.DonHangs", "TenXa", c => c.String());
            AddColumn("dbo.DonHangs", "TenHuyen", c => c.String());
            AddColumn("dbo.DonHangs", "TenTinh", c => c.String());
            AddColumn("dbo.DonHangs", "TrangThai", c => c.Int(nullable: false));
            AlterColumn("dbo.DonHangs", "CachThanhToan", c => c.Int(nullable: false));
            AlterColumn("dbo.DonHangs", "SoDienThoai", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.DonHangs", "CachVanChuyen");
            DropColumn("dbo.DonHangs", "DiaChi");
            DropColumn("dbo.DonHangs", "GiamGia");
            DropColumn("dbo.DonHangs", "CodeGiamGia");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DonHangs", "CodeGiamGia", c => c.String());
            AddColumn("dbo.DonHangs", "GiamGia", c => c.Double(nullable: false));
            AddColumn("dbo.DonHangs", "DiaChi", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.DonHangs", "CachVanChuyen", c => c.String());
            AlterColumn("dbo.DonHangs", "SoDienThoai", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.DonHangs", "CachThanhToan", c => c.String());
            DropColumn("dbo.DonHangs", "TrangThai");
            DropColumn("dbo.DonHangs", "TenTinh");
            DropColumn("dbo.DonHangs", "TenHuyen");
            DropColumn("dbo.DonHangs", "TenXa");
            DropColumn("dbo.DonHangs", "TenDuong");
            DropColumn("dbo.DonHangs", "SoNha");
        }
    }
}
