namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHoTenToTaiKhoan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaiKhoans", "HoTen", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaiKhoans", "HoTen");
        }
    }
}
