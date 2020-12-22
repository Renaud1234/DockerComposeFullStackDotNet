using Microsoft.EntityFrameworkCore;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Data
{
    public interface IDbContext
    {
        DbSet<WeatherForecast> WeatherForecasts { get; set; }

        void Migrate();

        int SaveChanges();
    }
}