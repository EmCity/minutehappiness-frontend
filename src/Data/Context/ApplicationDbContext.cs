using MinuteOfHappiness.Frontend.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MinuteOfHappiness.Frontend.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(string connectionString)
        {
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<VideoGroup> VideoGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Video configuration

            var videoConfig = modelBuilder.Entity<Video>();
            videoConfig.ToTable("VideoFragments");
            videoConfig.HasKey(model => model.Id);
            videoConfig.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            videoConfig.Property(model => model.Url).IsRequired().HasMaxLength(500);
            videoConfig.Property(model => model.StartSeconds).IsRequired();
            videoConfig.Property(model => model.EndSeconds).IsRequired();
            videoConfig.Ignore(model => model.StartTime);
            videoConfig.Ignore(model => model.EndTime);
            videoConfig.HasMany(model => model.Groups).WithMany(model => model.Videos).Map(conf =>
            {
                conf.ToTable("VideoGroupsMapping");
                conf.MapLeftKey("VideoId");
                conf.MapRightKey("GroupId");
            });

            #endregion

            #region VideoGroup configuration

            var videoGroupConfig = modelBuilder.Entity<VideoGroup>();
            videoGroupConfig.ToTable("VideoGroups");
            videoGroupConfig.HasKey(model => model.Id);
            videoGroupConfig.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            videoGroupConfig.Property(model => model.Name).IsOptional().HasMaxLength(200);


            #endregion
        }
    }
}
