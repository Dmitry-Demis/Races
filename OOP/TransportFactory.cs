using Race.Interfaces;
using Race.Speed;
using Race.Transport.Air;
using Race.Transport.Ground;
using Race.Utility;

namespace Race
{
    public static class TransportFactory
    {
        private static readonly Dictionary<TransportType, ITransport> _factories = new() 
        {
            {
                TransportType.Centaur,
                new Centaur(new GroundTransport(
                    name: "Кентавр",
                    speed: new ConstantSpeed(100),
                    timeBeforeRest: 1.0.H(),
                    restDurationFunction: stopNumber => stopNumber * 15.0.M())) // Реализация IGroundTransport
            },
            {
                TransportType.HutOnChickenLegs,
                new HutOnChickenLegs(new GroundTransport(
                    name: "Избушка на курьих ножках",
                    speed: new ConstantSpeed(70),
                    timeBeforeRest: 1.5.H(),
                    restDurationFunction: stopNumber => stopNumber * 12.0.M())
                )
            },
            {
                TransportType.PumpkinCarriage,
                new PumpkinCarriage(new GroundTransport(
                    name: "Карета-тыква",
                    speed: new ConstantSpeed(80),
                    timeBeforeRest: 1.0.H(),
                    restDurationFunction: stopNumber => stopNumber * 20.0.M())
                )
            },
            {
                TransportType.SwiftBoots,
                new SwiftBoots(new GroundTransport(
                    name: "Сапоги-скороходы",
                    speed: new ConstantSpeed(120),
                    timeBeforeRest: 1.0.H(),
                    restDurationFunction: stopNumber => stopNumber * 10.0.M())
                )
            },
            {
                TransportType.BabaYagaStupa,
                new BabaYagaStupa(new AirTransport(
                    name: "Ступа Бабы Яги",
                    speed: new LinearSpeed(15, 0.15) // Пример функции, меняющей ускорение
                ))
            },
            {
                TransportType.Broomstick,
                new Broomstick(new AirTransport(
                    name: "Метла",
                    speed: new LinearSpeed(15, 0.2)
                ))
            },
            {
                TransportType.FlyingCarpet,
                new FlyingCarpet(new AirTransport(
                    name: "Ковер-самолёт",
                    speed: new LinearSpeed(45, 0.05)
                ))
            },
            {
                TransportType.FlyingShip,
                new FlyingShip(new AirTransport(
                    name: "Летучий корабль",
                    speed: new LinearSpeed(60, 0.3)
                ))
            }
        };

        public static ITransport CreateTransport(TransportType transportType)
        {
            if (_factories.TryGetValue(transportType, out var transport))
                return transport;
            throw new ArgumentException($"Транспорт типа {transportType} не найден.");
        }
    }

}