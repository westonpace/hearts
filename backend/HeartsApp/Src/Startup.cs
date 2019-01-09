using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hearts.Application
{
    public class Startup
    {

        private Random masterRandom = new Random();

        public void ConfigureServices(IServiceCollection services)
        {
            // Framework
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });
            services.AddScoped<Random>((_) => new Random(masterRandom.Next()));
            services.AddDbContext<HeartsContext>(options =>
                options.UseNpgsql("Host=localhost;Database=hearts;Username=postgres;Password=Xsw23edc")
            );
            // Data
            services.AddScoped<AppStatisticsRepository>();
            // Hearts Domain
            services.AddScoped<Shuffler>();
            // Use cases
            services.AddScoped<GetShuffledDeckUseCase>();
            // Controllers
            services.AddScoped<HomeController>();

            // Migration
            var sp = services.BuildServiceProvider();
            var liveMigrations = new List<ILiveMigration>()
            {
                new SeedAppStatistics()
            };
            new LiveMigrationService(liveMigrations).MigrateAsync(sp).Wait();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
