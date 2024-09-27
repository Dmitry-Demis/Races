using Race.Interfaces;
using Race.Speed;

namespace Race.Transport
{
    public abstract class Transport(string name, ISpeed speed) : ITransport
    {
        public string Name { get; } = name;
        public ISpeed Speed { get; } = speed;

        public abstract double CalculateTime(double distance);
    }
}