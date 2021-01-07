using System;

namespace WeatherForecastAPI.DTOs.WeatherForecastViewModels
{
    public class WeatherForecastUpdate
    {
        public int Id { get; set; }
        //public DateTime Date { get; set; }    // Business rule: Weather Forecast Date can't be updated
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
    }
}