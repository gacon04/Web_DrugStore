namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHoatDongfieldToDanhMucs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DanhMucs", "HoatDong", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DanhMucs", "HoatDong");
        }
    }
}
