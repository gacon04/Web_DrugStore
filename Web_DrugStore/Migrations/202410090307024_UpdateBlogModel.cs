namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBlogModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "TieuDe", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Blogs", "NoiDung", c => c.String(nullable: false));
            AddColumn("dbo.Blogs", "IdTacGia", c => c.Int(nullable: false));
            AddColumn("dbo.Blogs", "DuongDanHinhAnh", c => c.String(maxLength: 255));
            CreateIndex("dbo.Blogs", "IdTacGia");
            AddForeignKey("dbo.Blogs", "IdTacGia", "dbo.TaiKhoans", "ID", cascadeDelete: true);
            DropColumn("dbo.Blogs", "Title");
            DropColumn("dbo.Blogs", "Content");
            DropColumn("dbo.Blogs", "AuthorId");
            DropColumn("dbo.Blogs", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blogs", "ImagePath", c => c.String(maxLength: 255));
            AddColumn("dbo.Blogs", "AuthorId", c => c.Int(nullable: false));
            AddColumn("dbo.Blogs", "Content", c => c.String(nullable: false));
            AddColumn("dbo.Blogs", "Title", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.Blogs", "IdTacGia", "dbo.TaiKhoans");
            DropIndex("dbo.Blogs", new[] { "IdTacGia" });
            DropColumn("dbo.Blogs", "DuongDanHinhAnh");
            DropColumn("dbo.Blogs", "IdTacGia");
            DropColumn("dbo.Blogs", "NoiDung");
            DropColumn("dbo.Blogs", "TieuDe");
        }
    }
}
