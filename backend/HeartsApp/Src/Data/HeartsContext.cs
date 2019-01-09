using Microsoft.EntityFrameworkCore;

namespace Hearts.Application
{
    public class HeartsContext : DbContext
    {

        public HeartsContext(DbContextOptions<HeartsContext> options) : base(options)
        {

        }

        public DbSet<AppStatistics> AppStatistics { get; set; }
        public DbSet<MigrationRecord> MigrationRecord { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}