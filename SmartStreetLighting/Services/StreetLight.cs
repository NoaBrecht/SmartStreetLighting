using SmartStreetLighting.Models;

namespace SmartStreetLighting
{
    internal class StreetLight : ILight
    {
        public bool IsEnabled => throw new NotImplementedException();

        public int Strength => throw new NotImplementedException();

        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void Enable(int Strength)
        {
            throw new NotImplementedException();
        }

        public string Status()
        {
            throw new NotImplementedException();
        }
    }
}
