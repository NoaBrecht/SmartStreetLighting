using Gherkin;
using SmartStreetLighting.Models;
using SmartStreetLighting.Services;
using System.Globalization;
using System.Security.Cryptography;
using Xunit;
using Xunit.Gherkin.Quick;

namespace SmartStreetLighting.AcceptanceTests.StepDefinitions
{
    [FeatureFile("./Features/StreetLightControllerFailure.Feature")]
    public sealed class StreetLightControllerStepsFailure : Feature
    {
        private const int MaxFailures = 5;
        private const int OffSet = 20;
        private const double SetPoint = 10000;

        private StreetLightController streetLightController;
        private ILight light;
        private ILightSensor lightsensor;
        private IWeatherSensor weathersensor;
        private ICurrenTime currentTime;
        private const string UrlMockoonLux = "http://localhost:3000/data/1.0/LightData";
        private const string UrlMockoonLuxException = "http://localhost:3000/data/1.0/LightData/exception";
        private const string UrlMockoonWeather = "http://localhost:3000/data/1.0/WeatherData";
        private const string UrlMockoonWeatherException = "http://localhost:3000/data/1.0/WeatherData/exception";

        public StreetLightControllerStepsFailure()
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
        [Given(@"streetLightController in safe mode")]
        public void SetstreetLightControllertatInsafeMode()
        {
            lightsensor.Url = $"{UrlMockoonLuxException}";
            for (int i = 0; i < MaxFailures; i++)
            {
                streetLightController.ManageLights();
            }
        }
        [When(@"the system receives successful lux data after max lux failures")]
        public void GetLuxWorks()
        {
            lightsensor.Url = $"{UrlMockoonLux}";
            weathersensor.Url = $"{UrlMockoonWeather}";
        }
        [When(@"getting the Lux fails")]
        public void GetLuxGivesException()
        {
            lightsensor.Url = $"{UrlMockoonLuxException}";
            streetLightController.ManageLights();
        }
        [When(@"getting the Weather fails")]
        public void GetWeatherGivesException()
        {
            lightsensor.Url = $"{UrlMockoonWeatherException}";
            streetLightController.ManageLights();
        }
        [And(@"number of failures is less than maximum")]
        public void ResetNumberOfFailures()
        {
            string queryParam = "?temp=" + (SetPoint).ToString(CultureInfo.InvariantCulture);
            lightsensor.Url = $"{UrlMockoonLux}{queryParam}";
            streetLightController.ManageLights();
        }
        [And(@"number of failures is maximum failures minus one")]
        public void SetMaximumNumberOfFailuresMinusOne()
        {
            string queryParam = "";
            lightsensor.Url = $"{UrlMockoonLuxException}{queryParam}";
            for (int i = 1; i < MaxFailures; i++)
            {
                streetLightController.ManageLights();
            }
        }
        [And(@"number of failures is maximum failures")]
        public void SetMaximumNumberOfFailures()
        {
            string queryParam = "";
            lightsensor.Url = $"{UrlMockoonLuxException}{queryParam}";
            for (int i = 1; i < MaxFailures; i++)
            {
                streetLightController.ManageLights();
            }
            streetLightController.ManageLights();
        }
        [Then(@"turn light on")]
        public void CheckLightOn()
        {
            Assert.True(light.IsEnabled);
        }
        [Then(@"turn light off")]
        public void CheckLightOff()
        {
            Assert.False(light.IsEnabled);
        }
        [And(@"set streetLightController in safe mode")]
        public void CheckstreetLightControllerInSafeMode()
        {
            Assert.True(streetLightController.InSafeMode);
        }
        [And(@"set streetLightController in normal mode")]
        public void CheckstreetLightControllerInNormalMode()
        {
            streetLightController.ManageLights();
            Assert.False(streetLightController.InSafeMode);
        }

    }
}
