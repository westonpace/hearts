using System.Threading.Tasks;

namespace Hearts.Application
{
    public class SeedAppStatistics : ILiveMigration
    {
        public string Name => "SeedAppStatistics";

        public async Task MigrateAsync(HeartsContext context)
        {
            await context.AppStatistics.AddAsync(new AppStatistics());
        }
    }
}