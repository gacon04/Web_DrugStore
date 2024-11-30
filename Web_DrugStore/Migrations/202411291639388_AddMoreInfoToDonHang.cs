namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreInfoToDonHang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHangs", "TongTienHang", c => c.Double(nullable: false));
            AddColumn("dbo.DonHangs", "TongHoaDon", c => c.Double(nullable: false));
            AddColumn("dbo.DonHangs", "VAT", c => c.Double(nullable: false));
            AddColumn("dbo.DonHangs", "GiamGia", c => c.Double(nullable: false));
            AddColumn("dbo.DonHangs", "CodeGiamGia", c => c.String());
            AddColumn("dbo.DonHangs", "PhiVanChuyen", c => c.Double(nullable: false));
            DropColumn("dbo.DonHangs", "TongTien");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DonHangs", "TongTien", c => c.Double(nullable: false));
            DropColumn("dbo.DonHangs", "PhiVanChuyen");
            DropColumn("dbo.DonHangs", "CodeGiamGia");
            DropColumn("dbo.DonHangs", "GiamGia");
            DropColumn("dbo.DonHangs", "VAT");
            DropColumn("dbo.DonHangs", "TongHoaDon");
            DropColumn("dbo.DonHangs", "TongTienHang");
        }
    }
}
