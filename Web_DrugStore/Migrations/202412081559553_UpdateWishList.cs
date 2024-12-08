namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWishList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DanhSachYeuThiches", "SanPhamId", "dbo.SanPhams");
            DropForeignKey("dbo.DanhSachYeuThiches", "TaiKhoanId", "dbo.TaiKhoans");
            DropIndex("dbo.DanhSachYeuThiches", new[] { "TaiKhoanId" });
            DropIndex("dbo.DanhSachYeuThiches", new[] { "SanPhamId" });
            CreateTable(
                "dbo.WishlistSanPham",
                c => new
                    {
                        WishlistId = c.Int(nullable: false),
                        SanPhamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WishlistId, t.SanPhamId })
                .ForeignKey("dbo.DanhSachYeuThiches", t => t.WishlistId, cascadeDelete: true)
                .ForeignKey("dbo.SanPhams", t => t.SanPhamId, cascadeDelete: true)
                .Index(t => t.WishlistId)
                .Index(t => t.SanPhamId);
            
            AddColumn("dbo.DanhSachYeuThiches", "UserAspId", c => c.String(nullable: false));
            DropColumn("dbo.DanhSachYeuThiches", "TaiKhoanId");
            DropColumn("dbo.DanhSachYeuThiches", "SanPhamId");
            DropColumn("dbo.DanhSachYeuThiches", "NgayThem");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DanhSachYeuThiches", "NgayThem", c => c.DateTime(nullable: false));
            AddColumn("dbo.DanhSachYeuThiches", "SanPhamId", c => c.Int(nullable: false));
            AddColumn("dbo.DanhSachYeuThiches", "TaiKhoanId", c => c.Int(nullable: false));
            DropForeignKey("dbo.WishlistSanPham", "SanPhamId", "dbo.SanPhams");
            DropForeignKey("dbo.WishlistSanPham", "WishlistId", "dbo.DanhSachYeuThiches");
            DropIndex("dbo.WishlistSanPham", new[] { "SanPhamId" });
            DropIndex("dbo.WishlistSanPham", new[] { "WishlistId" });
            DropColumn("dbo.DanhSachYeuThiches", "UserAspId");
            DropTable("dbo.WishlistSanPham");
            CreateIndex("dbo.DanhSachYeuThiches", "SanPhamId");
            CreateIndex("dbo.DanhSachYeuThiches", "TaiKhoanId");
            AddForeignKey("dbo.DanhSachYeuThiches", "TaiKhoanId", "dbo.TaiKhoans", "ID", cascadeDelete: true);
            AddForeignKey("dbo.DanhSachYeuThiches", "SanPhamId", "dbo.SanPhams", "SanPhamId", cascadeDelete: true);
        }
    }
}
