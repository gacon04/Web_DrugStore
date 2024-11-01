namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHoatDongToBlogCate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DanhMucBlogs", "HoatDong", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DanhMucBlogs", "HoatDong");
        }
    }
}
