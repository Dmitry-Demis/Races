namespace Race.Interfaces
{
    public interface IGroundTransport : ITransport
    {
        double TimeBeforeRest { get; }
        double GetRestDuration(int stopNumber);
    }
}