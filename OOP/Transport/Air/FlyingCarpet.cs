
namespace Race.Transport.Air
{
    public class FlyingCarpet(AirTransport transport)
        : AirTransport(transport.Name, transport.Speed)
    {

    }
}