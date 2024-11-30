namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAspIdToDonHang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHangs", "UserAspId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonHangs", "UserAspId");
        }
    }
}
