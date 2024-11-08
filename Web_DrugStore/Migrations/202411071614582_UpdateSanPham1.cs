namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSanPham1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SanPhams", "HinhAnh");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SanPhams", "HinhAnh", c => c.String());
        }
    }
}
