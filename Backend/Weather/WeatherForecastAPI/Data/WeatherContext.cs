using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Data
{
    public class WeatherContext : DbContext, IDbContext
    {
        private readonly IConfiguration _config;

        public WeatherContext(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbUser = _config["Database:DB_USER"] ?? "";
            var dbPassword = _config["Database:DB_PASSWORD"] ?? "";
            var dbHost = _config["Database:DB_HOST"] ?? "localhost";
            //var dbPort = _config["Database:DB_PORT"] ?? "1433";
            var dbName = "WeatherForecastDb";

            var connectionString = $"User Id={dbUser};Server={dbHost};Password={dbPassword};Database={dbName};Trusted_Connection=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }

        public void Migrate()
        {
            Database.Migrate();
        }
    }
}