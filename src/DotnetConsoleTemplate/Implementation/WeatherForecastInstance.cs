using DotnetConsoleTemplate.Entities;
using DotnetConsoleTemplate.Services;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace DotnetConsoleTemplate.Implementation
{
    public class WeatherForecastInstance(IConfiguration configuration, WeatherForecastService weatherForecastService, ILogger logger)
    {
        private readonly ILogger _logger = logger;
        private readonly IConfiguration _configuration = configuration;
        private readonly WeatherForecastService _weatherForecastService = weatherForecastService;

        public async Task Execute(int days)
        {
            var unit =
                   _configuration.GetValue<TemperatureUnitEnum>("MySettings:Unit");
            _logger.Information("Start get weather forecast.\n");

            var forecasts = await _weatherForecastService.GetForecasts(days);
            Console.WriteLine(string.Format("╭{0,10}¡{1,10}¡{2,10}╮", "----------", "----------", "----------"));
            Console.WriteLine(string.Format("|{0,10}|{1,10}|{2,10}|", "Date", "Temp", "Summary"));
            Console.WriteLine(string.Format("|{0,10}|{1,10}|{2,10}|", "----------", "----------", "----------"));
            var stringBuilder = new System.Text.StringBuilder();
            foreach (var forecast in forecasts)
            {
                var temperature = unit == TemperatureUnitEnum.Fahrenheit
                    ? forecast.TemperatureF
                    : forecast.TemperatureC;
                stringBuilder.Append(String.Format("|{0,10}|{1,10}|{2,10}|\n", forecast.Date.ToString("yyyy-MM-dd"), temperature.ToString() + (unit == TemperatureUnitEnum.Fahrenheit ? "°F" : "°C"), forecast.Summary));
                stringBuilder.Append(String.Format("|{0,10}|{1,10}|{2,10}|\n", "----------", "----------", "----------"));

            }
            Console.WriteLine(stringBuilder.ToString());
            _logger.Information("End get weather forecast.");
        }

    }
}