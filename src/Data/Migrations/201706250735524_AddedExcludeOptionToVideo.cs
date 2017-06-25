namespace MinuteOfHappiness.Frontend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedExcludeOptionToVideo : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.VideoFragments", newName: "ModelFragments");
            AddColumn("dbo.ModelFragments", "Exclude", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ModelFragments", "Exclude");
            RenameTable(name: "dbo.ModelFragments", newName: "VideoFragments");
        }
    }
}
