using Race.Interfaces;
using Race.Speed;
using Race.Weather;

namespace Race.Transport
{
    public abstract class Transport(string name, ISpeed speed, IWeather weather) : ITransport
    {
        public string Name { get; } = name;
        public ISpeed Speed { get; } = speed;

        public abstract double CalculateTime(double distance);
        public IWeather Weather { get; } = weather;
        public abstract double ApplyWeatherModifier();
    }
}