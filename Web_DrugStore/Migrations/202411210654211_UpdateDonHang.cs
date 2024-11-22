namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDonHang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHangs", "NgayGiao", c => c.DateTime());
            AddColumn("dbo.DonHangs", "CachThanhToan", c => c.String());
            AddColumn("dbo.DonHangs", "CachVanChuyen", c => c.String());
            DropColumn("dbo.SanPhams", "GiaGoc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SanPhams", "GiaGoc", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.DonHangs", "CachVanChuyen");
            DropColumn("dbo.DonHangs", "CachThanhToan");
            DropColumn("dbo.DonHangs", "NgayGiao");
        }
    }
}
