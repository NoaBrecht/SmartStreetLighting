using Moq;
using SmartStreetLighting.Models;
using SmartStreetLighting.Services;
using System.Security.Cryptography;

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
        public void TestLightDisableWhenLuxAboveSetPointNotWinterSunnyDay()
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
        public void TestLightDisableWhenLuxAboveSetPointWinterSunnyDay()
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
        public void TestLightEnableWhenLuxAboveSetPointWinterRainDay()
        {
            // Arrange
            lightSensorMock.Setup(x => x.GetLuxValue()).Returns((int)SetPoint);
            weatherSensorMock.Setup(x => x.GetWeatherCondition()).Returns("rain");
            currentTimeMock.Setup(x => x.GetCurrentHour()).Returns(12);
            currentTimeMock.Setup(x => x.IsWinterSeason()).Returns(false);

            // Act
            streetLightController.ManageLights();

            // Assert
            streetLightMock.Verify(x => x.Disable(), Times.Never);
            streetLightMock.Verify(x => x.Enable(4), Times.Once);
        }
        [TestMethod]
        public void TestLightEnabledWhenLuxBelowSetPointminusOffSet()
        {
            // Arrange
            lightSensorMock.Setup(x => x.GetLuxValue()).Returns((int)SetPoint-OffSet-1);
            weatherSensorMock.Setup(x => x.GetWeatherCondition()).Returns("sunny");
            currentTimeMock.Setup(x => x.GetCurrentHour()).Returns(12);
            currentTimeMock.Setup(x => x.IsWinterSeason()).Returns(false);

            // Act
            streetLightController.ManageLights();

            // Assert
            streetLightMock.Verify(x => x.Disable(), Times.Never);
            streetLightMock.Verify(x => x.Enable(3), Times.Once);
        }
        //[TestMethod]
        //public void TestLightEnableWhenLuxAboveSetPointNotWinterSunnyNight()
        //{
        //    // Arrange
        //    lightSensorMock.Setup(x => x.GetLuxValue()).Returns((int)SetPoint);
        //    weatherSensorMock.Setup(x => x.GetWeatherCondition()).Returns("Sunny");
        //    currentTimeMock.Setup(x => x.GetCurrentHour()).Returns(1);
        //    currentTimeMock.Setup(x => x.IsWinterSeason()).Returns(false);

        //    // Act
        //    streetLightController.ManageLights();

        //    // Assert

        //    streetLightMock.Verify(x => x.Disable(), Times.Never);
        //    streetLightMock.Verify(x => x.Enable(6), Times.Once);
        //}
        //[TestMethod]
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


        [TestMethod]
        public void TestSafeModeActivatedAfterMaxFailuresDueToTemperatureErrorLightSensor()
        {
            //Arrange
            lightSensorMock.Setup(x => x.GetLuxValue()).Throws<Exception>();
            for (int i = 1; i < streetLightController.MaxFailures; i++)
            {
                streetLightController.ManageLights();
            }

            // Act
            streetLightController.ManageLights();

            // Assert
            streetLightMock.Verify(x => x.Enable(It.IsAny<int>()), Times.Once);
            streetLightMock.Verify(x => x.Disable(), Times.Never);
            Assert.IsTrue(streetLightController.InSafeMode);
        }

        [TestMethod]
        public void TestSafeModeActivatedAfterMaxFailuresDueToTemperatureErrorWeatherensor()
        {
            //Arrange
            weatherSensorMock.Setup(x => x.GetWeatherCondition()).Throws<Exception>();
            for (int i = 1; i < streetLightController.MaxFailures; i++)
            {
                streetLightController.ManageLights();
            }

            // Act
            streetLightController.ManageLights();

            // Assert
            streetLightMock.Verify(x => x.Enable(It.IsAny<int>()), Times.Once);
            streetLightMock.Verify(x => x.Disable(), Times.Never);
            Assert.IsTrue(streetLightController.InSafeMode);
        }

        [TestMethod]
        public void TestExitSafeModeAfterSuccessfulRecoveryFromSensorErrorLightSensor()
        {
            // Arrange
            lightSensorMock.Setup(x => x.GetLuxValue()).Throws<Exception>();
            for (int i = 0; i < streetLightController.MaxFailures; i++)
            {
                streetLightController.ManageLights();
            }
            Assert.IsTrue(streetLightController.InSafeMode);
            lightSensorMock.Setup(x => x.GetLuxValue()).Returns((int)SetPoint);
            weatherSensorMock.Setup(x => x.GetWeatherCondition()).Returns("snow");
            currentTimeMock.Setup(x => x.GetCurrentHour()).Returns(2);
            currentTimeMock.Setup(x => x.IsWinterSeason()).Returns(true);

            // Act
            streetLightController.ManageLights();

            // Assert
            Assert.IsFalse(streetLightController.InSafeMode);
        }
        [TestMethod]
        public void TestExitSafeModeAfterSuccessfulRecoveryFromSensorErrorWeatherSensor()
        {
            // Arrange
            weatherSensorMock.Setup(x => x.GetWeatherCondition()).Throws<Exception>();
            for (int i = 0; i < streetLightController.MaxFailures; i++)
            {
                streetLightController.ManageLights();
            }
            Assert.IsTrue(streetLightController.InSafeMode);
            lightSensorMock.Setup(x => x.GetLuxValue()).Returns((int)SetPoint);
            weatherSensorMock.Setup(x => x.GetWeatherCondition()).Returns("snow");
            currentTimeMock.Setup(x => x.GetCurrentHour()).Returns(2);
            currentTimeMock.Setup(x => x.IsWinterSeason()).Returns(true);

            // Act
            streetLightController.ManageLights();

            // Assert
            Assert.IsFalse(streetLightController.InSafeMode);
        }
    }
}
