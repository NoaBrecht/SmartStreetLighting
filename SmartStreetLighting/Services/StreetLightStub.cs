using SmartStreetLighting.Models;

namespace SmartStreetLighting
{
    internal class StreetLightStub : ILight
    {
        private bool isEnabled;
        private int strength;

        public bool IsEnabled
        {
            get {  return isEnabled; }
        }
        public int Strength
        {
            get { return strength; }
        }
        public void Enable(int Strenght)
        {
            isEnabled = true;
            strength = Strenght;
            Console.WriteLine(Strength.ToString() + " " + isEnabled.ToString());
        }
        public void Disable()
        {
            isEnabled = false;
            strength = 0;
        }
        public string Status()
        {
            return $"{IsEnabled} {Strength}";
        }
    }
}
