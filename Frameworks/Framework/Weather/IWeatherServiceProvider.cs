using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Weather
{
    public interface IWeatherServiceProvider
    {
        double Longtitude { get; set; }
        double Latitude { get; set; }
        string CityName { get; set; }

        Task<Framework.Weather.WeatherDTO> GetWeather();
    }
}

