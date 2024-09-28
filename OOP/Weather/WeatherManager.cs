using Race.Interfaces;

namespace Race.Weather;

public static class WeatherManager
{
    private static readonly Random _random = new Random();

    private static readonly List<IWeather> _weatherOptions =
    [
        new SunnyWeather(),
        new RainyWeather(),
        new WindyWeather()
    ];

    public static IWeather GetRandomWeather()
    {
        // Выбираем случайное погодное условие из списка
        var index = _random.Next(_weatherOptions.Count);
        return _weatherOptions[index];
    }
}