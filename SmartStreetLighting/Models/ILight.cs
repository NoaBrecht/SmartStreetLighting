namespace SmartStreetLighting.Models
{
    public interface ILight
    {
        public bool IsEnabled { get; }
        public int Strength { get; }
        public string Status();
        public void Enable(int Strength);
        public void Disable();
    }
}
