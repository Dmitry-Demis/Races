using Race.Interfaces;

namespace Race.Weather;

public sealed class RainyWeather : IWeather
{
    public string Description => "Дождливая погода";

    public double SpeedModifier(ITransport transport) =>
        // Замедляем наземный транспорт, воздушный немного замедляем
        transport is IGroundTransport ? 0.8 : 0.9;
}