namespace SmartStreetLighting.Models
{
    public interface IWeatherSensor
    {
        string Url { get; set; }
        string GetWeatherCondition();

    }
}