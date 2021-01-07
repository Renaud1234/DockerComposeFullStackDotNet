using System;

namespace WeatherForecastAPI.DTOs.WeatherForecastViewModels
{
    public class WeatherForecastCreate
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
    }
}