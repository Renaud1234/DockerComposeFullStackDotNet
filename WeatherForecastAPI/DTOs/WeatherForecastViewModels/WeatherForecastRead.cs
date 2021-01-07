using System;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.DTOs.WeatherForecastViewModels
{
    public class WeatherForecastRead
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string Summary { get; set; }

        public WeatherForecastRead(WeatherForecast weatherForecast)
        {
            if (weatherForecast is null)
            {
                throw new ArgumentNullException(nameof(weatherForecast));
            }

            Id = weatherForecast.Id;
            Date = weatherForecast.Date;
            TemperatureC = weatherForecast.TemperatureC;
            Summary = weatherForecast.Summary;
        }
    }
}