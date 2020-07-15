namespace SP_ASPNET_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blogtablechanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BlogPost", "DateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BlogPost", "DateTime", c => c.DateTime(nullable: false));
        }
    }
}
