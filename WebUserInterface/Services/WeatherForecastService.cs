using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUserInterface.Models;
using WebUserInterface.Repositories;

namespace WebUserInterface.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly string RequestUri = "api/WeatherForecasts";
        private readonly string UriString = "http://localhost:5001";

        private readonly ApiGenericRepository<WeatherForecast> _repository;

        public WeatherForecastService()
        {
            _repository = new ApiGenericRepository<WeatherForecast>(new Uri($"{UriString}/{RequestUri}"));
        }

        public async Task<IEnumerable<WeatherForecast>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<WeatherForecast> GetOne(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task Create(WeatherForecast weatherForecast)
        {
            await _repository.PostAsync(weatherForecast);
        }

        public async Task Update(WeatherForecast weatherForecast)
        {
            await _repository.PutAsync(weatherForecast);
        }

        public async Task Remove(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public bool Exist(long id)
        {
            throw new NotImplementedException();
        }
    }
}