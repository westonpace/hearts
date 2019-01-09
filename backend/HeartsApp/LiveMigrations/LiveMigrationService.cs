using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hearts.Application
{
    public class LiveMigrationService
    {
        private List<ILiveMigration> migrations;

        public LiveMigrationService(List<ILiveMigration> migrations)
        {
            this.migrations = migrations;
        }

        public async Task MigrateAsync(IServiceProvider serviceProvider)
        {
            HashSet<string> existingMigrations;
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<HeartsContext>();
                existingMigrations = new HashSet<string>(await context.MigrationRecord.Select(record => record.Id).ToListAsync());
            }
            foreach (var migration in migrations)
            {
                if (!existingMigrations.Contains(migration.Name))
                {
                    await ApplyMigrationAsync(migration, serviceProvider);
                }
            }
        }

        private async Task ApplyMigrationAsync(ILiveMigration migration, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<HeartsContext>();
                await migration.MigrateAsync(context);
                await context.MigrationRecord.AddAsync(new MigrationRecord { Id = migration.Name });
                await context.SaveChangesAsync();
            }
        }

    }
}