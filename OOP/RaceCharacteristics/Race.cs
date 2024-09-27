using Race.Interfaces;

namespace Race.RaceCharacteristics;

public abstract class Race(double distance) : IRace
{
    protected readonly List<ITransport> Transports = [];
    public double Distance { get; } = distance;
    private string? winner_ { get; set; }

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

    public void StartRace()
    {
        if (Transports.Count == 0)
            throw new InvalidOperationException("Нет зарегистрированных транспортных средств для гонки.");

        var results = Transports
            .Select(t => new { Transport = t.Name, Time = t.CalculateTime(Distance) })
            .OrderBy(x => x.Time)
            .ToList();

        winner_ = results.First().Transport; // Возвращаем имя победителя
    }

    public string? Winner() => winner_;

    protected abstract bool IsTransportValid(ITransport transport);
}

public class GroundRace(double distance) : Race(distance)
{
    protected override bool IsTransportValid(ITransport transport) => transport is IGroundTransport;
}

public class AirRace(double distance) : Race(distance)
{
    protected override bool IsTransportValid(ITransport transport) => transport is IAirTransport;
}

public class MixedRace(double distance) : Race(distance)
{
    protected override bool IsTransportValid(ITransport transport) => true; // Все типы транспорта допустимы
}

public interface IRaceFactory
{
    IRace CreateRace(double distance);
}

public class GroundRaceFactory : IRaceFactory
{
    public IRace CreateRace(double distance)
    {
        return new GroundRace(distance);
    }
}

public class AirRaceFactory : IRaceFactory
{
    public IRace CreateRace(double distance)
    {
        return new AirRace(distance);
    }
}

public class MixedRaceFactory : IRaceFactory
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