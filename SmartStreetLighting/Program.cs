using SmartStreetLighting.Models;
using SmartStreetLighting.Services;
namespace SmartStreetLighting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IWeatherSensor weatherSensor = new WeatherSensorData();
            ILight light = new StreetLightStub();
            ILightSensor lightSensor = new LightSensorData();
            ICurrenTime time = new CurrentTime();

            StreetLightController streetLightController = new StreetLightController(light, weatherSensor,lightSensor, time);
            while (true)
            {
                streetLightController.ManageLights();
                Thread.Sleep(30000);
            }
        }
    }
}
