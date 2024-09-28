
namespace Race.Transport.Air
{
    public sealed class Broomstick(AirTransport transport)
        : AirTransport(transport.Name, transport.Speed, transport.Weather)
    {
       
    }
}