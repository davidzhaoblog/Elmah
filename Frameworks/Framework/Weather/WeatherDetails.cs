using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Weather
{
    public class WeatherDetails
    {
        public string Location { get; set; }
        public string WeatherIcon { get; set; }
        public double Temperature { get; set; }
        public double TemperatureMax { get; set; }
        public double TemperatureMin { get; set; }
        public double ChanceOfRain { get; set; }
        public double TemperatureC { get; set; }
        public double TemperatureF { get; set; }
        public double Humidity { get; set; }
        public string WeatherCondition { get; set; }
    }
}

