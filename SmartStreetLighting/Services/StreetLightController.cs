using SmartStreetLighting.Models;

namespace SmartStreetLighting.Services
{
    public class StreetLightController
    {
        private readonly ILight light;
        private readonly IWeatherSensor weatherSensor;
        private readonly ILightSensor lightSensor;
        private readonly ICurrenTime CurrentTime;

        public StreetLightController(ILight light, IWeatherSensor weatherSensor, ILightSensor lightSensor, ICurrenTime time)
        {
            this.light = light;
            this.weatherSensor = weatherSensor;
            this.lightSensor = lightSensor;
            this.CurrentTime = time;
        }
        private int failures = 0;
        private double setpoint;
        public double SetPoint
        {
            get { return setpoint; }
            set { setpoint = value; }
        }
        private double offset;
        public double Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        private int maxFailures;
        public int MaxFailures
        {
            get { return maxFailures; }
            set { maxFailures = value; }
        }
        public bool InSafeMode
        {
            get { return (failures < MaxFailures) ? false : true; }
        }
        public void ManageLights()
        {
            try
            {
                int time = CurrentTime.GetCurrentHour();
                bool isWinter = CurrentTime.IsWinterSeason();
                int lux = lightSensor.GetLuxValue();
                string weather = weatherSensor.GetWeatherCondition();
                weather = weather.ToLower();

                failures = 0;

                if (isWinter)
                {
                    if (time >= 20 || time <= 6)
                    {
                        if (weather == "snow")
                        {
                            light.Enable(8);
                        }
                        else if (weather == "fog")
                        {
                            light.Enable(9);
                        }
                        else if (weather == "storm")
                        {
                            light.Enable(10);
                        }
                        else if (weather == "cloudy")
                        {
                            light.Enable(5);
                        }
                        else
                        {
                            light.Enable(6);
                        }
                    }
                    else
                    {
                        if (weather == "snow")
                        {
                            light.Enable(5);
                        }
                        else if (weather == "fog" || weather == "storm")
                        {
                            light.Enable(6);
                        }
                        else if (weather == "cloudy")
                        {
                            light.Enable(3);
                        }
                        else if (lux < SetPoint - offset)
                        {
                            light.Enable(3);
                        }
                        else
                        {
                            light.Disable();
                        }
                    }
                }
                else
                {
                    if (time >= 18 || time <= 8)
                    {
                        if (weather == "snow")
                        {
                            light.Enable(8);
                        }
                        else if (weather == "fog")
                        {
                            light.Enable(9);
                        }
                        else if (weather == "storm")
                        {
                            light.Enable(10);
                        }
                        else if (weather == "cloudy")
                        {
                            light.Enable(5);
                        }
                        else
                        {
                            light.Enable(6);
                        }
                    }
                    else
                    {
                        if (weather == "snow")
                        {
                            light.Enable(5);
                        }
                        else if (weather == "fog" || weather == "storm")
                        {
                            light.Enable(6);
                        }
                        else if (weather == "cloudy")
                        {
                            light.Enable(3);
                        }
                        else if (lux < SetPoint - offset)
                        {
                            light.Enable(3);
                        }
                        else
                        {
                            light.Disable();
                        }
                    }
                }
            }
            catch (Exception)
            {
                failures++;
                int time = CurrentTime.GetCurrentHour();
                if (failures >= MaxFailures)
                {
                    if (time >= 20 || time <= 6)
                    {
                        light.Enable(7);
                    }
                    else
                    {
                        light.Enable(5);
                    }
                }
            }
        }

    }
}
