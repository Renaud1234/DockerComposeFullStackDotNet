using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebUserInterface.Models;

namespace WebUserInterface.Controllers
{
    public class WeatherForecastsController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly Uri Uri;

        public WeatherForecastsController()
        {
            // Update port # in the following line.
            Uri = new Uri("http://weatherforecastapi:80/api/WeatherForecasts/1");
            client.BaseAddress = new Uri("http://weatherforecastapi:80/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: WeatherForecasts
        public async Task<IActionResult> Index()
        {
            try
            {
                WeatherForecast weatherForecast = null;
                HttpResponseMessage response = await client.GetAsync(Uri.PathAndQuery);
                if (response.IsSuccessStatusCode)
                {
                    weatherForecast = await response.Content.ReadAsAsync<WeatherForecast>();
                }

                IList<WeatherForecast> list = new List<WeatherForecast>() { weatherForecast };
                return View(list);
            }
            catch(Exception)
            {
                return NotFound();
            }
        }
    }
}