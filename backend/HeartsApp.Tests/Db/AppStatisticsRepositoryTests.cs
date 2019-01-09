using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hearts.Application
{
    public class AppStatisticsRepositoryTest : DbTest
    {
        [SetUp]
        public async new Task Init()
        {
            using (var heartsContext = CreateHeartsContext())
            {
                heartsContext.AppStatistics.Add(new AppStatistics());
                await heartsContext.SaveChangesAsync();
            }
        }

        [Test]
        public async Task ShouldIncrementSafelyAcrossThreads()
        {
            var tasks = new Task[10];
            for (var i = 0; i < tasks.Length; i++)
            {
                tasks[i] = IncrementTask();
            }
            await Task.WhenAll(tasks);
            
            using (var heartsContext = CreateHeartsContext())
            {
                var repo = new AppStatisticsRepository(heartsContext, GetLogger<AppStatisticsRepository>());
                var stats = await repo.GetSingletonAsync();
                Assert.AreEqual(tasks.Length, stats.ShuffleCount);
            }
        }

        private async Task IncrementTask()
        {
            using(var heartsContext = CreateHeartsContext())
            {
                await new AppStatisticsRepository(heartsContext, GetLogger<AppStatisticsRepository>()).IncrementShuffleCount();
            }
        }

    }
}