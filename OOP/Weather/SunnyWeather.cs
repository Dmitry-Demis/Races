using Race.Interfaces;

namespace Race.Weather;

public class SunnyWeather : IWeather
{
    public string Description => "Солнечная погода";

    public double SpeedModifier(ITransport transport) =>
        // Если транспорт наземный, не изменяем скорость, а воздушный - ускоряем
        transport is IGroundTransport ? 1.0 : 1.1;
}