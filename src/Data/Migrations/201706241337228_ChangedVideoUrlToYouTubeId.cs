namespace MinuteOfHappiness.Frontend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedVideoUrlToYouTubeId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VideoFragments", "YouTubeId", c => c.String(nullable: false, maxLength: 500));
            DropColumn("dbo.VideoFragments", "Url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VideoFragments", "Url", c => c.String(nullable: false, maxLength: 500));
            DropColumn("dbo.VideoFragments", "YouTubeId");
        }
    }
}
