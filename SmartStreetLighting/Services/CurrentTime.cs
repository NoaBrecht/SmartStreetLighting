using SmartStreetLighting.Models;
namespace SmartStreetLighting.Services
{
    internal class CurrentTime : ICurrenTime
    {
        public int GetCurrentHour()
        {
            return DateTime.Now.Hour;
        }
        public int getCurrentMonth()
        {
            return DateTime.Now.Month;
        }
        public bool IsWinterSeason()
        {
            int month = getCurrentMonth();
            return month >= 10 || month <= 3;
        }
    }
}
