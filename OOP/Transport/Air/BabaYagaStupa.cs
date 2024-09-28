using Race.Transport.Air;

namespace Race
{
    public sealed class BabaYagaStupa(AirTransport transport)
        : AirTransport(transport.Name, transport.Speed, transport.Weather)
    {
        
    }
}