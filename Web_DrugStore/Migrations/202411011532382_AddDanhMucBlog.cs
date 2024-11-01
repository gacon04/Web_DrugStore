namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDanhMucBlog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DanhMucBlogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenDanhMuc = c.String(nullable: false, maxLength: 100),
                        MoTa = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Blogs", "DanhMucBlogId", c => c.Int(nullable: false));
            CreateIndex("dbo.Blogs", "DanhMucBlogId");
            AddForeignKey("dbo.Blogs", "DanhMucBlogId", "dbo.DanhMucBlogs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "DanhMucBlogId", "dbo.DanhMucBlogs");
            DropIndex("dbo.Blogs", new[] { "DanhMucBlogId" });
            DropColumn("dbo.Blogs", "DanhMucBlogId");
            DropTable("dbo.DanhMucBlogs");
        }
    }
}
