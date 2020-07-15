namespace SP_ASPNET_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogCommentChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPostComment",
                c => new
                    {
                        BlogPostCommentId = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(),
                        Comments = c.String(),
                        AuthorID = c.Int(nullable: false),
                        BlogPostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogPostCommentId)
                .ForeignKey("dbo.Author", t => t.AuthorID, cascadeDelete: true)
                .ForeignKey("dbo.BlogPost", t => t.BlogPostID, cascadeDelete: true)
                .Index(t => t.AuthorID)
                .Index(t => t.BlogPostID);
            
            AlterColumn("dbo.Author", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Author", "UserEmailid", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogPostComment", "BlogPostID", "dbo.BlogPost");
            DropForeignKey("dbo.BlogPostComment", "AuthorID", "dbo.Author");
            DropIndex("dbo.BlogPostComment", new[] { "BlogPostID" });
            DropIndex("dbo.BlogPostComment", new[] { "AuthorID" });
            AlterColumn("dbo.Author", "UserEmailid", c => c.String());
            AlterColumn("dbo.Author", "Password", c => c.String());
            DropTable("dbo.BlogPostComment");
        }
    }
}
