using Gherkin;
using SmartStreetLighting.Models;
using SmartStreetLighting.Services;
using System.Globalization;
using System.Security.Cryptography;
using Xunit;
using Xunit.Gherkin.Quick;

namespace SmartStreetLighting.AcceptanceTests.StepDefinitions
{
    [FeatureFile("./Features/StreetLightController.Feature")] 
    public sealed class StreetLightControllerDescisionSteps : Feature  
    {
        private const int MaxFailures = 5;
        private const int OffSet = 20;
        private const double SetPoint = 10000;

        private StreetLightController streetLightController;
        private ILight light;
        private ILightSensor lightsensor;
        private IWeatherSensor weathersensor;
        private ICurrenTime currentTime;
        private const string UrlMockoon = "http://localhost:3000/data/2.5/weather";
        private const string UrlMockoonException = "http://localhost:3000/data/2.5/weather/exception";

        public StreetLightControllerDescisionSteps()
        {
            lightsensor = new LightSensorData();
            light = new StreetLightStub();
            currentTime = new CurrentTimeStub();
            weathersensor = new WeatherSensorData();
            streetLightController = new StreetLightController(light, weathersensor, lightsensor, currentTime)
            {
                SetPoint = SetPoint,
                Offset = OffSet,
                MaxFailures = MaxFailures
            };
        }
        [Given(@"the heater is off")]
        [When(@"the temperature exceeds upper boundary")]
        public void SetHeaterOff()
        {
            string queryParam = "?temp=" + (SetPoint + OffSet).ToString(CultureInfo.InvariantCulture);
            lightsensor.Url = $"{UrlMockoon}{queryParam}";
            streetLightController.ManageLights();
        }
    }
}
