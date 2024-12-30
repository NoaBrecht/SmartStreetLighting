using Newtonsoft.Json;
using SmartStreetLighting.Models;
namespace SmartStreetLighting
{
    public class WeatherSensorData : IWeatherSensor
    {
        private string url = "http://api.openweathermap.org/data/2.5/weather?q=Antwerp,BE&appid=b1a90ec4d94d84ecf2a3f2bb634b970d&units=metric";
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        public string GetWeatherCondition()
        {
            using (var httpClient = new HttpClient())
            {
                var httpRespone = httpClient.GetAsync(url).GetAwaiter().GetResult();
                var response = httpRespone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var weatherData = JsonConvert.DeserializeObject<OpenWeather>(response);
                return weatherData.weather[0].main;
            }
        }
    }
}
