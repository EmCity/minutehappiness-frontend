namespace MinuteOfHappiness.Frontend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideoGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VideoFragments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 500),
                        StartSeconds = c.Int(nullable: false),
                        EndSeconds = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VideoGroupsMapping",
                c => new
                    {
                        VideoId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VideoId, t.GroupId })
                .ForeignKey("dbo.VideoFragments", t => t.VideoId, cascadeDelete: true)
                .ForeignKey("dbo.VideoGroups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.VideoId)
                .Index(t => t.GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoGroupsMapping", "GroupId", "dbo.VideoGroups");
            DropForeignKey("dbo.VideoGroupsMapping", "VideoId", "dbo.VideoFragments");
            DropIndex("dbo.VideoGroupsMapping", new[] { "GroupId" });
            DropIndex("dbo.VideoGroupsMapping", new[] { "VideoId" });
            DropTable("dbo.VideoGroupsMapping");
            DropTable("dbo.VideoFragments");
            DropTable("dbo.VideoGroups");
        }
    }
}
