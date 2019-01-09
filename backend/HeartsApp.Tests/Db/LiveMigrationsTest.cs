using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hearts.Application
{
    public class LiveMigrationsTest : DbTest
    {

        [Test]
        public async Task ShouldNotMigrateTwice()
        {
            var serviceProvider = CreateHeartsContextInServiceProvider();
            var migrations = new List<ILiveMigration>() { new SeedAppStatistics() };
            var migrationService = new LiveMigrationService(migrations);
            await migrationService.MigrateAsync(serviceProvider);
            await migrationService.MigrateAsync(serviceProvider);
            using (var heartsContext = CreateHeartsContext())
            {
                Assert.AreEqual(1, heartsContext.AppStatistics.Count());
            }
        }

    }
}