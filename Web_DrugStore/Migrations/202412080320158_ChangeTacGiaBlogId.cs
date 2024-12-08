namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTacGiaBlogId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Blogs", "IdTacGia", "dbo.TaiKhoans");
            DropIndex("dbo.Blogs", new[] { "IdTacGia" });
            AlterColumn("dbo.Blogs", "IdTacGia", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Blogs", "IdTacGia", c => c.Int(nullable: false));
            CreateIndex("dbo.Blogs", "IdTacGia");
            AddForeignKey("dbo.Blogs", "IdTacGia", "dbo.TaiKhoans", "ID", cascadeDelete: true);
        }
    }
}
