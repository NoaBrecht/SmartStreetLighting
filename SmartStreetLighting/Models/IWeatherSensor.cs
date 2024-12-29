namespace SmartStreetLighting.Models
{
    internal interface IWeatherSensor
    {
        string Url { get; set; }
        string GetWeatherCondition();

    }
}