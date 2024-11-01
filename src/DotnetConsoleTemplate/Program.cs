using DotnetConsoleTemplate.DataAccess;
using DotnetConsoleTemplate.Implementation;
using DotnetConsoleTemplate.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = Host.CreateDefaultBuilder(args);

#region Configuration
var configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
.Build();

builder.UseDefaultServiceProvider(options => options.ValidateScopes = false);

builder.ConfigureAppConfiguration((context, config) =>
{
    config.AddConfiguration(configuration);
});

Log.Logger = new LoggerConfiguration()
.ReadFrom.Configuration(configuration)
.CreateLogger();

builder.ConfigureServices((context, services) =>
{
    services.AddDbContext<GenerDbContext>(options =>
    {
        options.UseSqlServer(configuration["ConnectionStrings:SqlServer"]);
    });
    services.AddSingleton<WeatherForecastService>();
    services.AddSingleton<WeatherForecastInstance>();
    services.AddSingleton<CustomerService>();
});

builder.UseSerilog(Log.Logger);
#endregion

Log.Information("Start...");

Log.Information("Current environment: " + configuration["MySettings:Environment"]);

//Invoke
var host = builder.Build();
var weatherForecastService = host.Services.GetRequiredService<WeatherForecastInstance>();

await weatherForecastService.Execute(10);

//Database access service
// var customerService = host.Services.GetRequiredService<CustomerService>();
// await customerService.GetCustomerList();

Log.Information("End.");
Log.CloseAndFlush();
Console.ReadLine();