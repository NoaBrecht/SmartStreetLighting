using Moq;
using SmartStreetLighting.Models;
using SmartStreetLighting.Services;

namespace SmartStreetLighting.UnitTests
{
    [TestClass]
    public class StreetLightControllerTests
    {
        private const int MaxFailures = 5;
        private const int OffSet = 20;
        private const double SetPoint = 10000;

        private Mock<IWeatherSensor> weatherSensorMock = null;
        private Mock<ILightSensor> lightSensorMock = null;
        private Mock<ICurrenTime> currentTimeMock = null;
        private Mock<ILight> streetLightMock = null;

        private StreetLightController streetLightController;

        [TestInitialize]
        public void Initialize()
        {
            lightSensorMock = new Mock<ILightSensor>();
            weatherSensorMock = new Mock<IWeatherSensor>();
            currentTimeMock = new Mock<ICurrenTime>();
            streetLightMock = new Mock<ILight>();

            streetLightController = new StreetLightController(streetLightMock.Object, weatherSensorMock.Object, lightSensorMock.Object, currentTimeMock.Object)
            {
                SetPoint = SetPoint,
                Offset = OffSet,
                MaxFailures = MaxFailures
            };
        }
        [TestMethod]
        public void TestLightDisableWhenLuxAboveSetPointNotWinter()
        {
            // Arrange
            lightSensorMock.Setup(x => x.GetLuxValue()).Returns((int)SetPoint);
            weatherSensorMock.Setup(x => x.GetWeatherCondition()).Returns("Sunny");
            currentTimeMock.Setup(x => x.GetCurrentHour()).Returns(12);
            currentTimeMock.Setup(x => x.IsWinterSeason()).Returns(false);

            // Act
            streetLightController.ManageLights();

            // Assert

            streetLightMock.Verify(x => x.Enable(It.IsAny<int>()), Times.Never);
            streetLightMock.Verify(x => x.Disable(), Times.Once);
        }
        [TestMethod]
        public void TestLightDisableWhenLuxAboveSetPointInWinter()
        {
            // Arrange
            lightSensorMock.Setup(x => x.GetLuxValue()).Returns((int)SetPoint);
            weatherSensorMock.Setup(x => x.GetWeatherCondition()).Returns("sunny");
            currentTimeMock.Setup(x => x.GetCurrentHour()).Returns(12);
            currentTimeMock.Setup(x => x.IsWinterSeason()).Returns(true);

            // Act
            streetLightController.ManageLights();

            // Assert

            streetLightMock.Verify(x => x.Enable(It.IsAny<int>()), Times.Never);
            streetLightMock.Verify(x => x.Disable(), Times.Once);
        }
        [TestMethod]
        public void TestLightEnabledWithCorrectStrenghtInWinterWithSnowNightTime()
        {
            // Arrange
            lightSensorMock.Setup(x => x.GetLuxValue()).Returns((int)SetPoint);
            weatherSensorMock.Setup(x => x.GetWeatherCondition()).Returns("snow");
            currentTimeMock.Setup(x => x.GetCurrentHour()).Returns(2);
            currentTimeMock.Setup(x => x.IsWinterSeason()).Returns(true);

            // Act
            streetLightController.ManageLights();

            // Assert
            streetLightMock.Verify(x => x.Enable(8), Times.Once);
        }
    }
}
