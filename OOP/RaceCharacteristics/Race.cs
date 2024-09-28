using Race.Interfaces;

namespace Race.RaceCharacteristics;

/// <summary>
/// Класс, представляющий гонку
/// </summary>
/// <param name="distance">Дистанция гонки</param>
public abstract class Race(double distance) : IRace
{
    /// <summary>
    /// Список зарегистрированных транспортных средств
    /// </summary>
    protected readonly List<ITransport> Transports = [];

    /// <summary>
    /// Дистанция гонки
    /// </summary>
    public double Distance { get; } = distance;

    /// <summary>
    /// Имя победителя гонки
    /// </summary>
    private string? winner_;

    /// <summary>
    /// Регистрирует транспортное средство для участия в гонке
    /// </summary>
    /// <param name="transport">Транспортное средство для регистрации</param>
    public void RegisterTransport(ITransport transport)
    {
        if (!IsTransportValid(transport))
            throw new InvalidOperationException($"Неверный тип транспорта для этой гонки: {transport.Name}.");

        if (Transports.Contains(transport))
        {
            Console.WriteLine("Транспорт уже добавлен.");
            return;
        }
        Transports.Add(transport);
    }

    /// <summary>
    /// Запускает гонку, вычисляя время для каждого транспортного средства и определяя победителя
    /// </summary>
    public void StartRace()
    {
        if (Transports.Count == 0)
            throw new InvalidOperationException("Нет зарегистрированных транспортных средств для гонки.");

        var results = Transports
            .Select(t => new { Transport = t.Name, Time = t.CalculateTime(Distance) })
            .OrderBy(x => x.Time)
            .ToList();

        winner_ = results.First().Transport; // Устанавливаем имя победителя
    }

    /// <summary>
    /// Возвращает имя победителя гонки
    /// </summary>
    /// <returns>Имя победителя или null, если гонка еще не завершена</returns>
    public string? Winner() => winner_;

    /// <summary>
    /// Абстрактный метод для проверки, является ли транспорт допустимым для данной гонки
    /// </summary>
    /// <param name="transport">Транспортное средство</param>
    /// <returns>true, если транспорт допустим, иначе false</returns>
    protected abstract bool IsTransportValid(ITransport transport);
}


public sealed class GroundRace(double distance) : Race(distance)
{
    protected override bool IsTransportValid(ITransport transport) => transport is IGroundTransport;
}

public sealed class AirRace(double distance) : Race(distance)
{
    protected override bool IsTransportValid(ITransport transport) => transport is IAirTransport;
}

public sealed class MixedRace(double distance) : Race(distance)
{
    protected override bool IsTransportValid(ITransport transport) => true; // Все типы транспорта допустимы
}

public interface IRaceFactory
{
    IRace CreateRace(double distance);
}

public sealed class GroundRaceFactory : IRaceFactory
{
    public IRace CreateRace(double distance)
    {
        return new GroundRace(distance);
    }
}

public sealed class AirRaceFactory : IRaceFactory
{
    public IRace CreateRace(double distance)
    {
        return new AirRace(distance);
    }
}

public sealed class MixedRaceFactory : IRaceFactory
{
    public IRace CreateRace(double distance)
    {
        return new MixedRace(distance);
    }
}

public enum RaceType
{
    Ground = 1,
    Air,
    Mixed
}