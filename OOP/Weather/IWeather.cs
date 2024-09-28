using Race.Interfaces;

namespace Race.Weather;

public interface IWeather
{
    string Description { get; }

    // Универсальный метод для корректировки скорости
    double SpeedModifier(ITransport transport);
}