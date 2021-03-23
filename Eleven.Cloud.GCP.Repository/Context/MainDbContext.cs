using Eleven.Cloud.GCP.Entity;
using Microsoft.EntityFrameworkCore;

namespace Eleven.Cloud.GCP.Repository.Context
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Singer> Singer { get; set; }
    }
}
