using Newtonsoft.Json;
using SmartStreetLighting.Models;
namespace SmartStreetLighting.Services
{
    public class LightSensorData : ILightSensor
    {
        private string url = "http://localhost:3000/data/1.0/LightData";
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        public int GetLuxValue()
        {
            using (var httpClient = new HttpClient())
            {
                var httpRespone = httpClient.GetAsync(url).GetAwaiter().GetResult();
                var response = httpRespone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var lightsensorData = JsonConvert.DeserializeObject<LuxResponse>(response);
                Console.WriteLine(lightsensorData.Lux);
                return lightsensorData.Lux;
            }
        }
    }
}
