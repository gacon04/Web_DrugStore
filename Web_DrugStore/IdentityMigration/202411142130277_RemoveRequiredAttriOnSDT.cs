namespace Web_DrugStore.IdentityMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredAttriOnSDT : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "SDT", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "SDT", c => c.String(nullable: false));
        }
    }
}
