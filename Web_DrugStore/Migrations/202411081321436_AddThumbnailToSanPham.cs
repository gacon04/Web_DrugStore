namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddThumbnailToSanPham : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SanPhams", "Thumbnail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SanPhams", "Thumbnail");
        }
    }
}
