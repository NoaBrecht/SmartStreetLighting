using SmartStreetLighting.Models;
using SmartStreetLighting.Services;
using System.Globalization;

namespace SmartStreetLighting.IntegrationTests
{
    [TestClass]
    public sealed class StreetLightControllerTests
    {
        private const int MaxFailures = 5;
        private const int OffSet = 20;
        private const double SetPoint = 10000;
        private const string UrlMockoonLux = "http://localhost:3000/data/1.0/LightData";
        private const string UrlMockoonLuxException = "http://localhost:3000/data/1.0/LightData/exception";
        private const string UrlMockoonWeather = "http://localhost:3000/data/1.0/WeatherData";
        private const string UrlMockoonWeatherException = "http://localhost:3000/data/1.0/WeatherData/exception";

        private IWeatherSensor weatherSensor;
        private ILight light;
        private ILightSensor lightSensor;
        private ICurrenTime currenTime;

        private StreetLightController streetLightController;

        [TestInitialize]
        public void Initialize()
        {
            lightSensor = new LightSensorData();
            light = new StreetLightStub();
            weatherSensor = new WeatherSensorData();
            currenTime = new CurrentTimeStub();

            streetLightController = new StreetLightController(light, weatherSensor, lightSensor, currenTime)
            {
                SetPoint = SetPoint,
                Offset = OffSet,
                MaxFailures = MaxFailures
            };
        }

        [TestMethod]
        public void WhenLuxLessThenLowerBoundaryHeaterElementEnabled()
        {
            // Arrange
            string queryParam = "?lux=" + (SetPoint - OffSet).ToString(CultureInfo.InvariantCulture);
            lightSensor.Url = $"{UrlMockoonLux}{queryParam}";
            // Act
            streetLightController.ManageLights();

            // Assert
            Assert.IsTrue(light.IsEnabled);
        }
        [TestMethod]
        public void WhenLuxFailsAndMaxFailuresInSafeMode()
        {
            // Arrange
            string queryParam = "?temp=" + (SetPoint - OffSet).ToString(CultureInfo.InvariantCulture);
            lightSensor.Url = $"{UrlMockoonLux}{queryParam}";

            streetLightController.ManageLights();
            Assert.IsTrue(light.IsEnabled);

            lightSensor.Url = $"{UrlMockoonLuxException}";
            // number of failures = MaxFailures - 1
            for (int i = 1; i < streetLightController.MaxFailures; i++)
            {
                streetLightController.ManageLights();
            }

            // --- Act ---
            streetLightController.ManageLights();

            // --- Assert ---
            Assert.IsTrue(streetLightController.InSafeMode);
            Assert.IsTrue(light.IsEnabled);
        }
        [TestMethod]
        public void WhenInSafeModeAndLuxSuccesReset()
        {
            // Arrange
            lightSensor.Url = $"{UrlMockoonLuxException}";
            for (int i = 0; i < streetLightController.MaxFailures; i++)
            {
                streetLightController.ManageLights();
            }
            string queryParam = "?temp=" + (SetPoint).ToString(CultureInfo.InvariantCulture);
            lightSensor.Url = $"{UrlMockoonLux}{queryParam}";

            // --- Act ---
            streetLightController.ManageLights();

            // --- Assert ---
            Assert.IsFalse(streetLightController.InSafeMode);
        }

        [TestMethod]
        public void WhenWeatherFailsAndMaxFailuresInSafeMode()
        {
            // Arrange
            string queryParam = "?weather=rain";
            weatherSensor.Url = $"{UrlMockoonWeather}{queryParam}";

            streetLightController.ManageLights();
            Assert.IsFalse(light.IsEnabled);

            weatherSensor.Url = $"{UrlMockoonWeatherException}";
            for (int i = 1; i < streetLightController.MaxFailures; i++)
            {
                streetLightController.ManageLights();
            }

            // --- Act ---
            streetLightController.ManageLights();

            // --- Assert ---
            Assert.IsTrue(streetLightController.InSafeMode);
            Assert.IsTrue(light.IsEnabled);
        }
        [TestMethod]
        public void WhenInSafeModeAndWeatherSuccesReset()
        {
            // Arrange
            lightSensor.Url = $"{UrlMockoonLuxException}";
            for (int i = 0; i < streetLightController.MaxFailures; i++)
            {
                streetLightController.ManageLights();
            }
            string queryParam = "?temp=" + (SetPoint).ToString(CultureInfo.InvariantCulture);
            lightSensor.Url = $"{UrlMockoonLux}{queryParam}";

            // --- Act ---
            streetLightController.ManageLights();

            // --- Assert ---
            Assert.IsFalse(streetLightController.InSafeMode);
        }
    }
}
