
namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateSanPham : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SanPhams", "Hot", c => c.Boolean(nullable: false));
            AddColumn("dbo.SanPhams", "NgayTao", c => c.DateTime(nullable: false));
            AddColumn("dbo.SanPhams", "NgayCapNhat", c => c.DateTime());
        }

        public override void Down()
        {
            DropColumn("dbo.SanPhams", "NgayCapNhat");
            DropColumn("dbo.SanPhams", "NgayTao");
            DropColumn("dbo.SanPhams", "Hot");
        }
    }
}
