using Race.Interfaces;
using Race.Speed;
using Race.Transport.Air;
using Race.Transport.Ground;
using Race.Utility;
using Race.Weather;

namespace Race
{
    public static class TransportFactory
    {
        private static readonly Dictionary<TransportType, Func<IWeather, ITransport>> _factories = new() 
        {
            {
                TransportType.Centaur,
                weather => new Centaur(new GroundTransport(
                    name: "Кентавр",
                    speed: new ConstantSpeed(100),
                    timeBeforeRest: 1.0.H(),
                    restDurationFunction: stopNumber => stopNumber * 15.0.M()
                    , weather: weather)) // Реализация IGroundTransport
            },
            {
                TransportType.HutOnChickenLegs,
                weather => new HutOnChickenLegs(new GroundTransport(
                    name: "Избушка на курьих ножках",
                    speed: new ConstantSpeed(70),
                    timeBeforeRest: 1.5.H(),
                    restDurationFunction: stopNumber => stopNumber * 12.0.M(),
                    weather: weather)
                )
            },
            {
                TransportType.PumpkinCarriage,
                weather => new PumpkinCarriage(new GroundTransport(
                    name: "Карета-тыква",
                    speed: new ConstantSpeed(80),
                    timeBeforeRest: 1.0.H(),
                    restDurationFunction: stopNumber => stopNumber * 20.0.M()
                    , weather: weather)
                )
            },
            {
                TransportType.SwiftBoots,
                weather => new SwiftBoots(new GroundTransport(
                    name: "Сапоги-скороходы",
                    speed: new ConstantSpeed(120),
                    timeBeforeRest: 1.0.H(),
                    restDurationFunction: stopNumber => stopNumber * 10.0.M()
                    , weather: weather)
                )
            },
            {
                TransportType.BabaYagaStupa,
                weather => new BabaYagaStupa(new AirTransport(
                    name: "Ступа Бабы Яги",
                    speed: new LinearSpeed(15, 0.15) // Пример функции, меняющей ускорение
                    , weather: weather))
            },
            {
                TransportType.Broomstick,
                weather => new Broomstick(new AirTransport(
                    name: "Метла",
                    speed: new LinearSpeed(15, 0.2)
                    , weather: weather))
            },
            {
                TransportType.FlyingCarpet,
                weather => new FlyingCarpet(new AirTransport(
                    name: "Ковер-самолёт",
                    speed: new LinearSpeed(45, 0.05)
                    , weather: weather))
            },
            {
                TransportType.FlyingShip,
                weather => new FlyingShip(new AirTransport(
                    name: "Летучий корабль",
                    speed: new LinearSpeed(60, 0.3)
                    , weather: weather))
            }
        };

        public static ITransport CreateTransport(TransportType transportType, IWeather weather)
        {
            if (_factories.TryGetValue(transportType, out var createFunc))
                return createFunc(weather);
            throw new ArgumentException($"Транспорт типа {transportType} не найден.");
        }
    }

}