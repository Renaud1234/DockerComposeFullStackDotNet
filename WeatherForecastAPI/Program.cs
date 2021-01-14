using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Threading.Tasks;
using WeatherForecastAPI.Data;

namespace WeatherForecastAPI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var dbContext = services.GetRequiredService<WeatherDbContext>();
                await MigrateDbContextAsync(dbContext);
                await DbSeeding.InitializeAsync(dbContext);
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                throw;
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task MigrateDbContextAsync<TContext>(TContext dbContext)
            where TContext : DbContext
        {
            // Use Polly nuget to handle SqlException and retry 3 times the database migration
            if (dbContext.Database.IsSqlServer())
            {
                var policy = Policy
                  .Handle<SqlException>()
                  .WaitAndRetryAsync(new[]
                  {
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(8)
                  });

                await policy.ExecuteAsync(() => dbContext.Database.MigrateAsync());
            }
        }
    }
}