
namespace Race.Transport.Ground
{
    public class Centaur
        (GroundTransport transport)
        : GroundTransport(transport.Name, transport.Speed,
            transport.TimeBeforeRest, transport.RestDurationFunction)
    {

    }
}