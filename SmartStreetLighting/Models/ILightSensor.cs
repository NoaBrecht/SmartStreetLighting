namespace SmartStreetLighting.Models
{
    public interface ILightSensor
    {
        string Url { get; set; }
        int GetLuxValue();
    }
}
