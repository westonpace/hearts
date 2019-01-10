using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hearts.Application
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;

        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }

        private Random masterRandom = new Random();

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _config["Database:ConnectionString"];
            if (connectionString == null)
            {
                throw new Exception("The property Database:ConnectionString must be specified somehow");
            }
            // Framework
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });
            services.AddScoped<Random>((_) => new Random(masterRandom.Next()));
            services.AddDbContext<HeartsContext>(options =>
                options.UseNpgsql(connectionString)
            );
            // Data
            services.AddScoped<AppStatisticsRepository>();
            // Hearts Domain
            services.AddScoped<Shuffler>();
            // Use cases
            services.AddScoped<GetShuffledDeckUseCase>();
            // Controllers
            services.AddScoped<HomeController>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            // Migration
            var liveMigrations = new List<ILiveMigration>()
            {
                new SeedAppStatistics()
            };
            new LiveMigrationService(liveMigrations).MigrateAsync(serviceProvider).Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
