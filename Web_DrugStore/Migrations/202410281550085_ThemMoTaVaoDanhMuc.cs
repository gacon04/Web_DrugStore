namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemMoTaVaoDanhMuc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DanhMucs", "MoTa", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DanhMucs", "MoTa");
        }
    }
}
