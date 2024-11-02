using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace PUT_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMongoCollection<WeatherForecast> _weatherCollection;
        private readonly IConfiguration _config;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config)
        {
            _logger = logger;
            var connectionString = config.GetSection("DatabaseSettings:MongoConnectionString").Value;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("sample_mflix");
            _weatherCollection = database.GetCollection<WeatherForecast>("weatherForecasts");
            
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            var forecastsFromDb = _weatherCollection.Find(_ => true).ToList();

            if (forecastsFromDb.Any())
            {
                return forecastsFromDb;
            }
            return NotFound();
        }

        [HttpPost(Name = "PostWeatherForecast")]
        public IActionResult Post([FromBody] WeatherForecast newForecast)
        {
            if (newForecast == null)
            {
                return BadRequest("Weather forecast data is required.");
            }

            _weatherCollection.InsertOne(newForecast);

            return CreatedAtRoute("GetWeatherForecast", newForecast);
        }
    }
}
