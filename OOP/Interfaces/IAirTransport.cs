namespace Race.Interfaces
{
    public interface IAirTransport : ITransport
    {
        double AccelerationCoefficient { get; }
    }
}