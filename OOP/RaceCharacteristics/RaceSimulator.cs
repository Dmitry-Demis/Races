using Race.Interfaces;
using Race.Weather;

namespace Race.RaceCharacteristics;

public class RaceSimulator
{
    public void Start()
    {
        Console.WriteLine("Введите дистанцию гонки (в километрах) [100, 5000]:");

        if (!double.TryParse(Console.ReadLine(), out var distance) || distance is < 100 or > 5000)
            throw new ArgumentException("Неверная дистанция гонки.");


        Console.WriteLine("Выберите тип гонки:");
        Console.WriteLine("1 - Наземные ТС");
        Console.WriteLine("2 - Воздушные ТС");
        Console.WriteLine("3 - Все виды ТС");

        if (!(Enum.TryParse<RaceType>(Console.ReadLine(), out var raceType) && Enum.IsDefined(typeof(RaceType), raceType)))
            throw new ArgumentException("Неверный тип гонки.");


        RaceManager.Instance.CreateRace(raceType, distance);
        var raceWeather = WeatherManager.GetRandomWeather();
        while (true)
        {
            Console.WriteLine(new string('=', 15));
            Console.WriteLine("Выберите транспортное средство для регистрации:");
            Console.WriteLine("1 - Ступа Бабы Яги (В); 2 - Метла (В); 3 - Сапоги-скороходы (Н); 4 - Карета-тыква (Н);");
            Console.WriteLine("5 - Ковер-самолёт (В); 6 - Избушка на курьих ножках (Н); 7 - Кентавр (Н); 8 - Летучий корабль (В);");
            Console.WriteLine("Чтобы завершить выбор, введите 0.");
            Console.WriteLine(new string('=', 15));

            if (!int.TryParse(Console.ReadLine(), out var transportChoice) 
                || transportChoice < 0 || transportChoice > Enum.GetValues(typeof(TransportType)).Length + 1)
            {
                Console.WriteLine("Ошибка: введите число от 0 до 8.");
                continue;
            }

            if (transportChoice == 0)
            {
                Console.WriteLine("Регистрация завершена.");
                break;
            }
            
            Console.WriteLine($"Погода для гонки: {raceWeather.Description}");
            try
            {
                var transport = TransportFactory.CreateTransport((TransportType)transportChoice, raceWeather);
                RaceManager.Instance.RegisterTransport(transport);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка регистрации: {ex.Message}");
            }
        }
        try
        {
            RaceManager.Instance.StartCurrentRace();
            Console.WriteLine($"Победителем является: {RaceManager.Instance.Winner()}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}