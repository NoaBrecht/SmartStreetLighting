using Newtonsoft.Json;
using SmartStreetLighting.Models;
namespace SmartStreetLighting.Services
{
    internal class LightSensorData : ILightSensor
    {
        private string url = "";
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        public int GetLuxValue()
        {
            using (var httpClient = new HttpClient())
            {
                //var httpRespone = httpClient.GetAsync(url).GetAwaiter().GetResult();
                //var response = httpRespone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                //var lightsensorData = JsonConvert.DeserializeObject<LuxResponse>(response);
                return 20;
            }
        }
    }
}
