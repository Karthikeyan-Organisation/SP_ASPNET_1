namespace SP_ASPNET_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blogchange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BlogPost", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BlogPost", "DateTime", c => c.DateTime());
        }
    }
}
