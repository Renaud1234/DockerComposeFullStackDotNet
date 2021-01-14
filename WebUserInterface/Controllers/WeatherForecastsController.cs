using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUserInterface.Models;
using WebUserInterface.Services;

namespace WebUserInterface.Controllers
{
    public class WeatherForecastsController : Controller
    {
        //private readonly WebUIContext _context;
        private readonly IWeatherForecastService _service;
    
        public WeatherForecastsController(IWeatherForecastService service)
        {
            _service = service;
        }
    
        // GET: WeatherForecasts
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _service.GetAll());
            }
            catch (Exception)
            {
                return View(new WeatherForecast());
            }
        }

        // GET: WeatherForecasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var weatherForecast = await _context.WeatherForecast
            //    .FirstOrDefaultAsync(m => m.Id == id);
            WeatherForecast weatherForecast = await _service.GetOne(id.GetValueOrDefault());
            if (weatherForecast == null)
            {
                return NotFound();
            }

            return View(weatherForecast);
        }

        // GET: WeatherForecasts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeatherForecasts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,TemperatureC,Summary")] WeatherForecast weatherForecast)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(weatherForecast);
                //await _context.SaveChangesAsync();
                await _service.Create(weatherForecast);
                return RedirectToAction(nameof(Index));
            }
            return View(weatherForecast);
        }

        // GET: WeatherForecasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var weatherForecast = await _context.WeatherForecast.FindAsync(id);
            WeatherForecast weatherForecast = await _service.GetOne(id.GetValueOrDefault());
            if (weatherForecast == null)
            {
                return NotFound();
            }
            return View(weatherForecast);
        }

        // POST: WeatherForecasts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,TemperatureC,Summary")] WeatherForecast weatherForecast)
        {
            if (id != weatherForecast.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(weatherForecast);
                    //await _context.SaveChangesAsync();
                    await _service.Update(weatherForecast);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!WeatherForecastExists(weatherForecast.Id))
                    if (!_service.Exist(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(weatherForecast);
        }

        // GET: WeatherForecasts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var weatherForecast = await _context.WeatherForecast
            //    .FirstOrDefaultAsync(m => m.Id == id);
            WeatherForecast weatherForecast = await _service.GetOne((int)id.GetValueOrDefault());
            if (weatherForecast == null)
            {
                return NotFound();
            }

            return View(weatherForecast);
        }

        // POST: WeatherForecasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var weatherForecast = await _context.WeatherForecast.FindAsync(id);
            //_context.WeatherForecast.Remove(weatherForecast);
            //await _context.SaveChangesAsync();
            await _service.Remove((int)id);
            return RedirectToAction(nameof(Index));
        }
    }
}