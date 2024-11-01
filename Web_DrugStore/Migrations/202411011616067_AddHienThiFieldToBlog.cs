namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHienThiFieldToBlog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "HienThi", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "HienThi");
        }
    }
}
