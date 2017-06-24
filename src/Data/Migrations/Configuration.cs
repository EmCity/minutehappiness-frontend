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
                model => model.Id,
                new Video { Id = 1, Url = "https://www.youtube.com/watch?v=hCnO7OkbQ10", StartSeconds = 10, EndSeconds = 25 },
                new Video { Id = 2, Url = "https://www.youtube.com/watch?v=rJjHhjCUXI0", StartSeconds = 15, EndSeconds = 35 },
                new Video { Id = 3, Url = "https://www.youtube.com/watch?v=TRA_TJ1a5M0", StartSeconds = 20, EndSeconds = 45 });
        }
    }
}