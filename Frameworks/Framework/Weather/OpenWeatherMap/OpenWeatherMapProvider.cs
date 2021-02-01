using System;
using System.Threading.Tasks;

namespace Framework.Weather.OpenWeatherMap
{
    public class OpenWeatherMapProvider : Framework.Weather.IWeatherServiceProvider
    {
        public double Longtitude { get; set; } = 0.0d;
        public double Latitude { get; set; } = 0.0d;
        public string CityName { get; set; } = string.Empty;

        public async Task<Framework.Weather.WeatherDTO> GetWeather()
        {
            var retval = new Framework.Weather.WeatherDTO();

            //Sign up for a free API key at http://openweathermap.org/appid
            string key = "dd93bb1183fe52148d430f3e2c7415e3";
            string queryString = $"https://api.openweathermap.org/data/2.5/weather?lat={this.Latitude}&lon={this.Longtitude}&appid={key}"; // &units=metric (but by default, it's set in Kelvin);

            if (this.Longtitude <= 180)
            {
                queryString = $"https://api.openweathermap.org/data/2.5/weather?lat={this.Latitude}&lon={this.Longtitude}&appid={key}";
            }
            else
            {
                queryString = $"https://api.openweathermap.org/data/2.5/weather?q={this.CityName}&appid={key}";
            }

            var result = await Framework.Weather.LiteWebClient.Request<Framework.Weather.OpenWeatherMap.OpenWeatherMapDTO>(queryString);

            if (result != null && result.cod == 200)
            {
                retval.WeatherDetails.Location = result.name;
                retval.WeatherDetails.Temperature = result.main.temp;
                retval.WeatherDetails.TemperatureC = ToCelcius(result.main.temp, result); // default unit is set to Kelvin
                retval.WeatherDetails.TemperatureF = ToFahrenheit(result.main.temp, result); // default unit is set to Kelvin
                retval.WeatherDetails.WeatherIcon = $"http://openweathermap.org/img/w/{result.weather[0].icon}.png";
                retval.WeatherDetails.TemperatureMax = ToCelcius(result.main.temp_max, result);
                retval.WeatherDetails.TemperatureMin = ToCelcius(result.main.temp_min, result);
                retval.WeatherDetails.Humidity = result.main.humidity;
                retval.WeatherDetails.WeatherCondition = result.weather[0].main.ToLower();
            }
            else
            {
                retval.WeatherDetails.WeatherCondition = "error";
            }

            return retval;
        }

        double ToCelcius(double kelvin, Framework.Weather.OpenWeatherMap.OpenWeatherMapDTO result)
        {
            return Math.Round(kelvin - 273.15, 1);
        }

        double ToFahrenheit(double kelvin, Framework.Weather.OpenWeatherMap.OpenWeatherMapDTO result)
        {
            return Math.Round((((9 / 5) * (kelvin - 273)) + 32), 1);
        }
    }
}

