
namespace Race.Transport.Air
{
    public class FlyingShip(AirTransport transport)
        : AirTransport(transport.Name, transport.Speed, transport.Weather)
    {

    }
}