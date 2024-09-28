using Race.Interfaces;

namespace Race.RaceCharacteristics;

public sealed class RaceManager
{
    private static readonly Lazy<RaceManager> _instance = new(() => new RaceManager());
    public static RaceManager Instance => _instance.Value;

    private IRace? _currentRace;
    private readonly Dictionary<RaceType, IRaceFactory> _raceFactories;

    private RaceManager()
    {
        _raceFactories = new Dictionary<RaceType, IRaceFactory>
        {
            { RaceType.Ground, new GroundRaceFactory() },
            { RaceType.Air, new AirRaceFactory() },
            { RaceType.Mixed, new MixedRaceFactory() }
        };
    }

    public void CreateRace(RaceType raceType, double distance)
    {
        if (_currentRace is not null)
            throw new InvalidOperationException("Гонка уже создана. Завершите текущую гонку перед созданием новой.");

        if (_raceFactories.TryGetValue(raceType, out var factory))
        {
            _currentRace = factory.CreateRace(distance);
        }
        else
        {
            throw new ArgumentException("Неверный тип гонки.");
        }
    }

    public void RegisterTransport(ITransport transport)
    {
        _currentRace?.RegisterTransport(transport);
    }

    public void StartCurrentRace()
    {
        _currentRace?.StartRace();
    }

    public string? Winner()
    {
        var s =  _currentRace?.Winner();
        _currentRace = null; // Сбрасываем текущую гонку после завершения
        return s;
    }
}