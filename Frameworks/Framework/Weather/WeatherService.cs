using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Weather
{
    public class WeatherService
    {
        Framework.Weather.IWeatherServiceProvider _WeatherProvider;

        public WeatherService()
        {

        }

        public void InitWeatherServiceProvider(Framework.Weather.IWeatherServiceProvider weatherProvider)
        {
            this._WeatherProvider = weatherProvider;
        }

        public async Task<Framework.Weather.WeatherDTO> GetForecastToday(double latitude, double longtitude, string cityName)
        {
            if (this._WeatherProvider == null)
                throw new NotImplementedException();

            var retval = new Framework.Weather.WeatherDTO();

            _WeatherProvider.Longtitude = longtitude;
            _WeatherProvider.Latitude = latitude;
            //_WeatherProvider.CityName = cityName;

            retval = await _WeatherProvider.GetWeather();
            retval.Status = true;

            if (retval != null)
            {
                //retval.WeatherDetails.Location += " weather";

                if (retval.WeatherDetails.WeatherCondition.Contains("cloud"))
                {
                    retval.Message = "Beautiful sunsets need cloudy skies...";
                }
                else if (retval.WeatherDetails.WeatherCondition.Contains("rain"))
                {
                    retval.Message = "Bring a raincoat. It's gonna get wet!";
                }
                else if (retval.WeatherDetails.WeatherCondition.Contains("snow"))
                {
                    retval.Message = "Brace yourself, winter is coming!";
                }
                else if (retval.WeatherDetails.WeatherCondition.Contains("clear"))
                {
                    retval.Message = "Beautiful clear sky";
                }
                else if (retval.WeatherDetails.WeatherCondition == "error")
                {
                    retval.Message = "Unable to fetch weather forecast in your location";
                    retval.Status = false;
                }
            }

            return retval;
        }
    }
    public class WeatherServiceSingleton: Singleton<WeatherService>
    {
    }
}

