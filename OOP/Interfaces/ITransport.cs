using Race.Speed;

namespace Race.Interfaces
{
    public interface ITransport
    {
        string Name { get; }
        ISpeed Speed { get; }
        double CalculateTime(double distance);
    }
}