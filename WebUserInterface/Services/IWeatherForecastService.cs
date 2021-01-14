using System.Collections.Generic;
using System.Threading.Tasks;
using WebUserInterface.Models;

namespace WebUserInterface.Services
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecast>> GetAll();

        Task<WeatherForecast> GetOne(int id);

        Task Create(WeatherForecast weatherForecast);

        Task Update(WeatherForecast weatherForecast);

        Task Remove(int id);

        bool Exist(long id);
    }
}