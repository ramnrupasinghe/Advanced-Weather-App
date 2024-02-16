using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WeatherApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Weather App!");
            Console.Write("Please enter your city name: ");
            string city = Console.ReadLine();

            try
            {
                string apiKey = "YOUR_API_KEY"; // Replace with your OpenWeatherMap API key
                string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);

                        string cityName = data.name;
                        string weatherDescription = data.weather[0].description;
                        double temperature = data.main.temp;
                        double humidity = data.main.humidity;

                        Console.WriteLine($"\nWeather in {cityName}:");
                        Console.WriteLine($"Description: {weatherDescription}");
                        Console.WriteLine($"Temperature: {temperature}°C");
                        Console.WriteLine($"Humidity: {humidity}%");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve weather data. Error: {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
