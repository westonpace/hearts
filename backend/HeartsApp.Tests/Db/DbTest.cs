using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Hearts.Application
{

    public class DbTest : BaseTest
    {

        [SetUp]
        public async new Task Init()
        {
            using (var context = CreateHeartsContext())
            {
                await context.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE \"AppStatistics\" RESTART IDENTITY");
                await context.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE \"MigrationRecord\" RESTART IDENTITY");
            }
        }

        protected HeartsContext CreateHeartsContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HeartsContext>();
            ConfigureDatabase(optionsBuilder);
            return new HeartsContext(optionsBuilder.Options);
        }

        protected IServiceProvider CreateHeartsContextInServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton(loggerFactory);
            services.AddDbContext<HeartsContext>(options =>
            {
                ConfigureDatabase(options);
            });
            return services.BuildServiceProvider();
        }

        private void ConfigureDatabase(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
              .UseNpgsql(connectionString)
              .UseLoggerFactory(loggerFactory);
        }

    }

}