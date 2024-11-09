namespace Web_DrugStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUserNameTaiKhoan : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TaiKhoans", "TenNguoiDung");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaiKhoans", "TenNguoiDung", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
