namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHoatDongToSanPham : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SanPhams", "HoatDong", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SanPhams", "HoatDong");
        }
    }
}
