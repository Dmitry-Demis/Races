namespace OOP
{
    public enum TransportType
    {
        BabaYagaStupa,     // Ступа Бабы Яги
        Broomstick,        // Метла
        SwiftBoots,        // Сапоги-скороходы
        PumpkinCarriage,   // Карета-тыква
        FlyingCarpet,      // Ковер-самолет
        HutOnChickenLegs,  // Избушка на курьих ножках
        Centaur,           // Кентавр
        FlyingShip         // Летучий корабль
    }
    public static class TimeExtensions
    {
        public static double H(this int hours) => hours; // Простой метод для часов
        public static double M(this int minutes) => TimeSpan.FromMinutes(minutes).TotalHours; // Перевод минут в часы
    }

    public interface ISpeed
    {
        double GetSpeed(double distance); // Теперь принимает расстояние
    }
    public abstract class Speed : ISpeed
    {
        public abstract double GetSpeed(double distance); // Определяем метод, который нужно реализовать
    }

    public class ConstantSpeed(double speed) : Speed
    {
        public override double GetSpeed(double distance) => speed;
    }

    public class LinearSpeed(double initialSpeed, double acceleration) : Speed
    {
        public override double GetSpeed(double distance)
        {
            throw new NotImplementedException();
        }
    }

    public interface ITransport
    {
        string Name { get; }
        ISpeed Speed { get; }
        double CalculateTime(double distance);
    }

    public interface IGroundTransport : ITransport
    {
        double TimeBeforeRest { get; }
        double GetRestDuration(int stopNumber);
    }

    public interface IAirTransport : ITransport
    {
        double AccelerationCoefficient { get; }
    }

    public abstract class Transport(string name) : ITransport
    {
        public string Name { get; } = name;
        public ISpeed Speed { get; }
        public abstract double CalculateTime(double distance);
    }

    public class GroundTransport(
        string name,
        ISpeed speed,
        double timeBeforeRest,
        Func<double, double> restDurationFunction)
        : IGroundTransport
    {
        public string Name { get; } = name;
        public ISpeed Speed { get; } = speed;
        public double TimeBeforeRest { get; } = timeBeforeRest;
        public Func<double, double> RestDurationFunction { get; } = restDurationFunction;

        public virtual double CalculateTime(double distance)
        {
            // Получаем текущую скорость на основании расстояния
            var speed = Speed.GetSpeed(distance);

            // Инициализируем переменные для отслеживания пройденного расстояния, общего времени и количества остановок
            double accumulatedDistance = 0;
            double time = 0;
            var stopCount = 0;

            // Цикл продолжается, пока пройденное расстояние меньше общего расстояния
            while (accumulatedDistance < distance)
            {
                // Проверяем, можно ли проехать оставшееся расстояние без остановок
                if (distance - accumulatedDistance < speed * TimeBeforeRest)
                {
                    // Если оставшееся расстояние меньше, чем максимальное расстояние, которое можно проехать без остановки,
                    // добавляем время, необходимое для его преодоления
                    time += (distance - accumulatedDistance) / speed;

                    // Обновляем общее пройденное расстояние до полного
                    accumulatedDistance = distance; // Полное расстояние пройдено
                }
                else
                {
                    // Если необходимо сделать остановку, добавляем время до следующей остановки
                    time += TimeBeforeRest;

                    // Обновляем общее пройденное расстояние, прибавляя расстояние, пройденное за время до остановки
                    accumulatedDistance += speed * TimeBeforeRest;

                    // Увеличиваем счётчик остановок
                    ++stopCount;

                    // Добавляем время, необходимое для остановки, с использованием функции, определяющей время отдыха
                    time += RestDurationFunction(stopCount);
                }
            }

            return time;
        }

        public double GetRestDuration(int stopNumber) => RestDurationFunction(stopNumber);
    }

    public abstract class AirTransport(string name, double accelerationCoefficient) : Transport(name), IAirTransport
    {
        public double AccelerationCoefficient { get; } = accelerationCoefficient;
    }

    public class Centaur
        (GroundTransport transport) 
        : GroundTransport(transport.Name, transport.Speed, 
            transport.TimeBeforeRest, transport.RestDurationFunction)
    {

    }
    public static class TransportFactory
    {
        private static readonly Dictionary<TransportType, ITransport> _factories = new()
        {
            {
                TransportType.Centaur,
                new Centaur(new GroundTransport(
                    name: "Кентавр", 
                    speed: new ConstantSpeed(50), 
                    timeBeforeRest: 1.H(),
                    restDurationFunction: stopNumber => 1.5 * stopNumber)) // Реализация IGroundTransport
            },
        };

        public static ITransport CreateTransport(TransportType transportType)
        {
            if (_factories.TryGetValue(transportType, out var transport))
                return transport;
            throw new ArgumentException($"Транспорт типа {transportType} не найден.");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            const int distance = 524;
            if (TransportFactory.CreateTransport(TransportType.Centaur) is not GroundTransport centaur) return;
            var t = centaur.CalculateTime(distance);
            Console.WriteLine(t);
            Console.WriteLine($"{centaur.Name}, {centaur.TimeBeforeRest}, {centaur.Speed}, {centaur.RestDurationFunction(1)}");

            var a = TimeSpan.FromMinutes(27).TotalHours;

        }
    }
}
