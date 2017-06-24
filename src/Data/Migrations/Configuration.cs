namespace MinuteOfHappiness.Frontend.Data.Migrations
{
    using MinuteOfHappiness.Frontend.Data.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MinuteOfHappiness.Frontend.Data.Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MinuteOfHappiness.Frontend.Data.Context.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Videos.AddOrUpdate(
                model => model.YouTubeId,
                new Video {  YouTubeId = "hCnO7OkbQ10", StartSeconds = 10, EndSeconds = 25 },
                new Video {  YouTubeId = "rJjHhjCUXI0", StartSeconds = 15, EndSeconds = 35 },
                new Video {  YouTubeId = "TRA_TJ1a5M0", StartSeconds = 20, EndSeconds = 45 },
                new Video {  YouTubeId = "LVKJfLbz5yQ", StartSeconds = 10, EndSeconds = 25 },
                new Video {  YouTubeId = "LP_1VAcXoBM", StartSeconds = 15, EndSeconds = 35 },
                new Video {  YouTubeId = "_NmDq_ZbrC0", StartSeconds = 20, EndSeconds = 45 },
                new Video {  YouTubeId = "y6Sxv-sUYtM", StartSeconds = 10, EndSeconds = 25 },
                new Video {  YouTubeId = "JRMOMjCoR58", StartSeconds = 15, EndSeconds = 35 },
                new Video {  YouTubeId = "MOWDb2TBYDg", StartSeconds = 20, EndSeconds = 45 },
                new Video {  YouTubeId = "qQkBeOisNM0", StartSeconds = 10, EndSeconds = 25 },
                new Video {  YouTubeId = "jKbR7u8J5PU", StartSeconds = 15, EndSeconds = 35 },
                new Video {  YouTubeId = "Sv6dMFF_yts", StartSeconds = 20, EndSeconds = 45 });
        }
    }
}
