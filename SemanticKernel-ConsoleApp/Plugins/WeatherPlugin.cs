using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;

namespace SemanticKernel_ConsoleApp.Plugins;

public class WeatherPlugin
{
    private readonly List<WeatherModel> weatherData = new List<WeatherModel>
    {
        new WeatherModel { City = "New York", Temperature = "22°C", Condition = "Sunny" },
        new WeatherModel { City = "Los Angeles", Temperature = "25°C", Condition = "Sunny" },
        new WeatherModel { City = "Chicago", Temperature = "18°C", Condition = "Cloudy" },
        new WeatherModel { City = "Houston", Temperature = "30°C", Condition = "Rainy" },
        new WeatherModel { City = "Miami", Temperature = "28°C", Condition = "Sunny" }
    };

    [KernelFunction("get_weather_by_city")]
    public WeatherModel GetWeatherByCity(string city)
    {
        var weather = weatherData.FirstOrDefault(w => w.City.Equals(city, StringComparison.OrdinalIgnoreCase));
        if (weather == null)
        {
            throw new ArgumentException($"Weather data for {city} not found.");
        }
        return weather;
    }

}

public class WeatherModel
    {
    public string City { get; set; }
    public string Temperature { get; set; }
    public string Condition { get; set; }
}