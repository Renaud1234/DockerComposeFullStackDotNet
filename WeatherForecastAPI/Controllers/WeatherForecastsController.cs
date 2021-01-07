using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecastAPI.Data;
using WeatherForecastAPI.DTOs.WeatherForecastViewModels;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastsController : ControllerBase
    {
        private readonly WeatherDbContext _context;

        public WeatherForecastsController(WeatherDbContext context)
        {
            _context = context;
        }

        // GET: api/WeatherForecasts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecastRead>>> GetWeatherForecasts()
        {
            var weatherForecast = _context.WeatherForecasts
                                          .AsQueryable();

            IQueryable<WeatherForecastRead> weatherForecastReads = from w in weatherForecast
                                                                   select new WeatherForecastRead(w);

            return await weatherForecastReads.ToListAsync();
            //return await _context.WeatherForecasts.ToListAsync();
        }

        // GET: api/WeatherForecasts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecastRead>> GetWeatherForecast(int id)
        {
            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);

            if (weatherForecast == null)
            {
                return NotFound();
            }

            return new WeatherForecastRead(weatherForecast);
        }

        // PUT: api/WeatherForecasts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherForecast(int id, WeatherForecastUpdate weatherForecastUpdate)
        {
            if (id != weatherForecastUpdate.Id)
            {
                return BadRequest();
            }

            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);

            if (weatherForecast == null)
            {
                return BadRequest();
            }

            weatherForecast.TemperatureC = weatherForecastUpdate.TemperatureC;
            weatherForecast.Summary = weatherForecastUpdate.Summary;

            _context.Entry(weatherForecast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherForecastExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WeatherForecasts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WeatherForecastRead>> PostWeatherForecast(WeatherForecastCreate weatherForecastCreate)
        {
            var weatherForecast = new WeatherForecast()
            {
                Date = weatherForecastCreate.Date,
                TemperatureC = weatherForecastCreate.TemperatureC,
                Summary = weatherForecastCreate.Summary
            };

            _context.WeatherForecasts.Add(weatherForecast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeatherForecast",
                                   new { id = weatherForecast.Id },
                                   new WeatherForecastRead(weatherForecast));
        }

        // DELETE: api/WeatherForecasts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeatherForecast(int id)
        {
            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);
            if (weatherForecast == null)
            {
                return NotFound();
            }

            _context.WeatherForecasts.Remove(weatherForecast);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeatherForecastExists(int id)
        {
            return _context.WeatherForecasts.Any(e => e.Id == id);
        }
    }
}