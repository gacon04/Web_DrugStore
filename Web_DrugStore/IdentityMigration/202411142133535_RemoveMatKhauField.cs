namespace Web_DrugStore.IdentityMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMatKhauField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "MatKhau");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "MatKhau", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
