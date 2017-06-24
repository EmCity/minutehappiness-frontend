using MinuteOfHappiness.Frontend.Data.Context;
using System.Data.Entity.Infrastructure;

namespace MinuteOfHappiness.Frontend.Data.Factory
{
    public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create()
        {
            return new ApplicationDbContext("DefaultConnection");
        }
    }
}
