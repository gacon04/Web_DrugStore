namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNoteToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHangs", "DiaChiChoLam", c => c.String(maxLength: 255));
            AddColumn("dbo.DonHangs", "GhiChu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonHangs", "GhiChu");
            DropColumn("dbo.DonHangs", "DiaChiChoLam");
        }
    }
}
