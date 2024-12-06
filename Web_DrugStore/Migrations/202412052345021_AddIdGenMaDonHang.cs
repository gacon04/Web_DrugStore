namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdGenMaDonHang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHangs", "MaDonHang", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonHangs", "MaDonHang");
        }
    }
}
