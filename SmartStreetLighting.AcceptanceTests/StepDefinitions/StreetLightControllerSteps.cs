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
        private const string UrlMockoonLux = "http://localhost:3000/data/1.0/LightData";
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
        [Given(@"the light is off")]
        [When(@"the lux exceeds the lower boundary")]
        public void SetLightOn()
        {
            string queryParam = "?luxStrenght=" + (SetPoint - OffSet - 10).ToString(CultureInfo.InvariantCulture);
            lightsensor.Url = $"{UrlMockoonLux}{queryParam}";
            streetLightController.ManageLights();
        }
        [Given(@"the light is on")]
        [When(@"the lux exceeds the upper boundary")]
        public void SetLightOff()
        {
            // Lux value higher than SetPoint + offset (light should be turned off)
            string queryParam = "?luxStrenght=" + (SetPoint - OffSet + 1).ToString(CultureInfo.InvariantCulture);
            lightsensor.Url = $"{UrlMockoonLux}{queryParam}";
            streetLightController.ManageLights();
        }


        [Then(@"the street light should turn on")]
        public void CheckStreetLightOn()
        {
            streetLightController.ManageLights();
            Assert.True(light.IsEnabled);
        }
        [Then(@"the street light should turn off")]
        public void CheckStreetLightOff()
        {
            streetLightController.ManageLights();
            Assert.False(light.IsEnabled);
        }
        [Then(@"light strenght should be 7")]
        public void CheckStrenght7()
        {
            streetLightController.ManageLights();
            Assert.True(light.Strength == 7);
        }
        [Then(@"light strength should be 1")]
        public void CheckStrength1()
        {
            streetLightController.ManageLights();
            Assert.True(light.Strength == 1);
        }

        [Then(@"light strength should be 2")]
        public void CheckStrength2()
        {
            streetLightController.ManageLights();
            Assert.True(light.Strength == 2);
        }

        [Then(@"light strength should be 3")]
        public void CheckStrength3()
        {
            streetLightController.ManageLights();
            Assert.True(light.Strength == 3);
        }

        [Then(@"light strength should be 4")]
        public void CheckStrength4()
        {
            streetLightController.ManageLights();
            Assert.True(light.Strength == 4);
        }

        [Then(@"light strength should be 5")]
        public void CheckStrength5()
        {
            streetLightController.ManageLights();
            Assert.True(light.Strength == 5);
        }

        [Then(@"light strength should be 6")]
        public void CheckStrength6()
        {
            streetLightController.ManageLights();
            Assert.True(light.Strength == 6);
        }

        [Then(@"light strength should be 7")]
        public void CheckStrength7()
        {
            streetLightController.ManageLights();
            Assert.True(light.Strength == 7);
        }

        [Then(@"light strength should be 8")]
        public void CheckStrength8()
        {
            streetLightController.ManageLights();
            Assert.True(light.Strength == 8);
        }

        [Then(@"light strength should be 9")]
        public void CheckStrength9()
        {
            streetLightController.ManageLights();
            Assert.True(light.Strength == 9);
        }

        [Then(@"light strength should be 10")]
        public void CheckStrength10()
        {
            streetLightController.ManageLights();
            Assert.True(light.Strength == 10);
        }

    }
}
