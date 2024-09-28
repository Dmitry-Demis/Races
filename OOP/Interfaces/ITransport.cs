using Race.Speed;
using Race.Weather;

namespace Race.Interfaces
{
    public interface ITransport
    {
        string Name { get; }
        ISpeed Speed { get; }
        double CalculateTime(double distance);
        IWeather Weather { get; }
        double ApplyWeatherModifier();
    }
}