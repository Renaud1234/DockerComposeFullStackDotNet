using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WeatherForecastAPI.Data;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDbContext _dbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                         IDbContext weatherContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbContext = weatherContext ?? throw new ArgumentNullException(nameof(weatherContext));
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var forecasts = _dbContext.WeatherForecasts;

            return forecasts;
        }

        [HttpPost]
        public IActionResult Post([FromBody] WeatherForecast weatherForecast)
        {
            _dbContext.WeatherForecasts.Add(weatherForecast);
            _dbContext.SaveChanges();
            return new OkResult();
        }
    }
}