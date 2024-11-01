using DotnetConsoleTemplate.Models;
using Serilog;

namespace DotnetConsoleTemplate.Services
{
    public class WeatherForecastService
    {
        private readonly ILogger _logger;

        public WeatherForecastService(ILogger logger)
        {
            _logger = logger;
        }

        private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];
        public async Task<IEnumerable<WeatherForecast>> GetForecasts(int count)
        {
            _logger.Debug("Load weather forecast: {Count}", count);
            var rng = new Random();
            var forecasts = Enumerable.Range(1, count).Select(index =>
                new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                });

            return await Task.FromResult(forecasts);
        }
    }
}