namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFullSanPhamFieldAndHinhAnhSP : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SanPhams", "DanhMuc_Id", "dbo.DanhMucs");
            DropIndex("dbo.SanPhams", new[] { "DanhMuc_Id" });
            RenameColumn(table: "dbo.SanPhams", name: "DanhMuc_Id", newName: "DanhMucId");
            CreateTable(
                "dbo.HinhAnhSanPhams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SanPhamId = c.Int(nullable: false),
                        DuongDan = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SanPhams", t => t.SanPhamId, cascadeDelete: true)
                .Index(t => t.SanPhamId);
            
            AddColumn("dbo.SanPhams", "PhanLoai", c => c.String(nullable: false));
            AddColumn("dbo.SanPhams", "CongDung", c => c.String());
            AddColumn("dbo.SanPhams", "QuyCach", c => c.String());
            AddColumn("dbo.SanPhams", "LuuY", c => c.String());
            AddColumn("dbo.SanPhams", "NhaSanXuat", c => c.String(nullable: false));
            AlterColumn("dbo.SanPhams", "TenSanPham", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.SanPhams", "DanhMucId", c => c.Int(nullable: false));
            CreateIndex("dbo.SanPhams", "DanhMucId");
            AddForeignKey("dbo.SanPhams", "DanhMucId", "dbo.DanhMucs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SanPhams", "DanhMucId", "dbo.DanhMucs");
            DropForeignKey("dbo.HinhAnhSanPhams", "SanPhamId", "dbo.SanPhams");
            DropIndex("dbo.HinhAnhSanPhams", new[] { "SanPhamId" });
            DropIndex("dbo.SanPhams", new[] { "DanhMucId" });
            AlterColumn("dbo.SanPhams", "DanhMucId", c => c.Int());
            AlterColumn("dbo.SanPhams", "TenSanPham", c => c.String(nullable: false));
            DropColumn("dbo.SanPhams", "NhaSanXuat");
            DropColumn("dbo.SanPhams", "LuuY");
            DropColumn("dbo.SanPhams", "QuyCach");
            DropColumn("dbo.SanPhams", "CongDung");
            DropColumn("dbo.SanPhams", "PhanLoai");
            DropTable("dbo.HinhAnhSanPhams");
            RenameColumn(table: "dbo.SanPhams", name: "DanhMucId", newName: "DanhMuc_Id");
            CreateIndex("dbo.SanPhams", "DanhMuc_Id");
            AddForeignKey("dbo.SanPhams", "DanhMuc_Id", "dbo.DanhMucs", "Id");
        }
    }
}
