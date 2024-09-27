using Race.Interfaces;

namespace Race.RaceCharacteristics
{
    public interface IRace
    {
        void RegisterTransport(ITransport transport);
        void StartRace();
        string? Winner();
    }
   


}