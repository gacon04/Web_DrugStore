namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLuotYeuThichANDLuotMuaToProd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SanPhams", "LuotYeuThich", c => c.Int(nullable: false));
            AddColumn("dbo.SanPhams", "LuotMua", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SanPhams", "LuotMua");
            DropColumn("dbo.SanPhams", "LuotYeuThich");
        }
    }
}
