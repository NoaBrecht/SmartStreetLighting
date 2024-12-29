namespace SmartStreetLighting.Models
{
    public class LuxResponse
    {
        public int Lux { get; set; }
    }
    public class OpenWeather
    {
        public Weather[] weather { get; set; }
    }
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}