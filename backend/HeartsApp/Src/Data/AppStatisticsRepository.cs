using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hearts.Application
{
    public class AppStatisticsRepository
    {
        private HeartsContext heartsContext;
        private ILogger<AppStatisticsRepository> logger;

        public AppStatisticsRepository(HeartsContext heartsContext, ILogger<AppStatisticsRepository> logger)
        {
            this.heartsContext = heartsContext;
            this.logger = logger;
        }

        public Task<AppStatistics> GetSingletonAsync()
        {
            return this.heartsContext.AppStatistics.FirstAsync();
        }

        public async Task<int> IncrementShuffleCount()
        {
            var appStatistics = await GetSingletonAsync();
            var result = ++appStatistics.ShuffleCount;
            heartsContext.Update(appStatistics);
            await OptimisticRetry.SaveChangesWithRetry(
                heartsContext,
                async (entries) =>
                {
                    var dbValues = await entries[0].GetDatabaseValuesAsync();
                    var dbValue = (int) dbValues["ShuffleCount"];
                    result = dbValue + 1;
                    entries[0].CurrentValues["ShuffleCount"] = result;
                    entries[0].OriginalValues["ShuffleCount"] = dbValue;
                },
                logger
            );
            return result;
        }
    }
}