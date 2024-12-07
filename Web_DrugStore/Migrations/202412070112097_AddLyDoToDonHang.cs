namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLyDoToDonHang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHangs", "LyDo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonHangs", "LyDo");
        }
    }
}
