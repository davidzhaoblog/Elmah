using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml
{
    public class WeatherVM: ViewModelBase
    {
        private WeatherDetails _WeatherDetails;
        public WeatherDetails WeatherDetails
        {
            get { return _WeatherDetails; }
            set { Set(nameof(WeatherDetails), ref _WeatherDetails, value); }
        }

        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { Set(nameof(Message), ref _Message, value); }
        }
    }

    public class WeatherDetails: Framework.Models.PropertyChangedNotifier
    {
        private string _Location = string.Empty;
        public string Location
        {
            get { return _Location; }
            set { Set(nameof(Location), ref _Location, value); }
        }

        private string _WeatherIcon = string.Empty;
        public string WeatherIcon
        {
            get { return _WeatherIcon; }
            set { Set(nameof(WeatherIcon), ref _WeatherIcon, value); }
        }

        private double _Temperature = 0.0d;
        public double Temperature
        {
            get { return _Temperature; }
            set { Set(nameof(Temperature), ref _Temperature, value); }
        }

        private double _TemperatureMax = 0.0d;
        public double TemperatureMax
        {
            get { return _TemperatureMax; }
            set { Set(nameof(TemperatureMax), ref _TemperatureMax, value); }
        }

        private double _TemperatureMin = 0.0d;
        public double TemperatureMin
        {
            get { return _TemperatureMin; }
            set { Set(nameof(TemperatureMin), ref _TemperatureMin, value); }
        }

        private double _ChanceOfRain = 0.0d;
        public double ChanceOfRain
        {
            get { return _ChanceOfRain; }
            set { Set(nameof(ChanceOfRain), ref _ChanceOfRain, value); }
        }

        private double _TemperatureC = 0.0d;
        public double TemperatureC
        {
            get { return _TemperatureC; }
            set { Set(nameof(TemperatureC), ref _TemperatureC, value); }
        }

        private double _TemperatureF = 0.0d;
        public double TemperatureF
        {
            get { return _TemperatureF; }
            set { Set(nameof(TemperatureF), ref _TemperatureF, value); }
        }

        private double _Humidity = 0.0d;
        public double Humidity
        {
            get { return _Humidity; }
            set { Set(nameof(Humidity), ref _Humidity, value); }
        }

        private string _WeatherCondition = string.Empty;
        public string WeatherCondition
        {
            get { return _WeatherCondition; }
            set { Set(nameof(WeatherCondition), ref _WeatherCondition, value); }
        }
    }
}

