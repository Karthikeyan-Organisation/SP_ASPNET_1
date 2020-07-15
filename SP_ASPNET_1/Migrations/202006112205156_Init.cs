namespace SP_ASPNET_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        AuthorID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Password = c.String(),
                        UserEmailid = c.String(),
                        RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.AuthorID)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.BlogPost",
                c => new
                    {
                        BlogPostID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Content = c.String(),
                        ImageUrl = c.String(),
                        Like = c.Int(),
                        AuthorID = c.Int(),
                    })
                .PrimaryKey(t => t.BlogPostID)
                .ForeignKey("dbo.Author", t => t.AuthorID)
                .Index(t => t.AuthorID);
            
            CreateTable(
                "dbo.ProductItem",
                c => new
                    {
                        ProductItemID = c.Int(nullable: false, identity: true),
                        ImageURL = c.String(),
                        ImageAlt = c.String(),
                        Title = c.String(),
                        ProductLine_ProductLineID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductItemID)
                .ForeignKey("dbo.ProductLine", t => t.ProductLine_ProductLineID)
                .Index(t => t.ProductLine_ProductLineID);
            
            CreateTable(
                "dbo.ProductLine",
                c => new
                    {
                        ProductLineID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductLineID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductItem", "ProductLine_ProductLineID", "dbo.ProductLine");
            DropForeignKey("dbo.BlogPost", "AuthorID", "dbo.Author");
            DropForeignKey("dbo.Author", "RoleId", "dbo.Roles");
            DropIndex("dbo.ProductItem", new[] { "ProductLine_ProductLineID" });
            DropIndex("dbo.BlogPost", new[] { "AuthorID" });
            DropIndex("dbo.Author", new[] { "RoleId" });
            DropTable("dbo.ProductLine");
            DropTable("dbo.ProductItem");
            DropTable("dbo.BlogPost");
            DropTable("dbo.Roles");
            DropTable("dbo.Author");
        }
    }
}
