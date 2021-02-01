namespace Framework.Weather
{
    public class WeatherDTO
    {
        public Framework.Weather.WeatherDetails WeatherDetails { get; set; } = new Framework.Weather.WeatherDetails();
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}

