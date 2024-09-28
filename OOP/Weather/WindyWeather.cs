using Race.Interfaces;

namespace Race.Weather;

public class WindyWeather : IWeather
{
    public string Description => "Ветреная погода";

    public double SpeedModifier(ITransport transport) =>
        // Воздушный транспорт ускоряется, наземный замедляется
        transport is IGroundTransport ? 0.9 : 1.2;
}