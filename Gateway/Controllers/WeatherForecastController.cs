using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gateway.AddressInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IAddressServiceClient _addressServiceClient;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            IAddressServiceClient addressServiceClient,
            ILogger<WeatherForecastController> logger)
        {
            _addressServiceClient = addressServiceClient ?? throw new ArgumentNullException(nameof(addressServiceClient));
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var result = await _addressServiceClient.GetAddressAsync("ratan");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
