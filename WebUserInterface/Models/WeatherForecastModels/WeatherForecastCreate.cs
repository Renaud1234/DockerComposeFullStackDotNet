using System;

namespace WebUserInterface.Models.WeatherForecastViewModels
{
    public class WeatherForecastCreate
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
    }
}