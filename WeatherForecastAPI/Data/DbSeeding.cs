using System;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Data
{
    public class DbSeeding
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static async Task InitializeAsync(WeatherDbContext context)
        {
            if (context.WeatherForecasts.Any() == false)
            {
                context.WeatherForecasts.AddRange(
                    GetWeatherForecastData());

                await context.SaveChangesAsync();
            }
        }

        public static WeatherForecast[] GetWeatherForecastData()
        {
            var rng = new Random();

            return Enumerable.Range(1, 10).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();
        }
    }
}