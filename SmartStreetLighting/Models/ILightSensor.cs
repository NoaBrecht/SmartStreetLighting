namespace SmartStreetLighting.Models
{
    internal interface ILightSensor
    {
        string Url { get; set; }
        int GetLuxValue();
    }
}
