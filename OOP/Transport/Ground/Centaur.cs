
namespace Race.Transport.Ground
{
    public sealed class Centaur
        (GroundTransport transport)
        : GroundTransport(transport.Name, transport.Speed,
            transport.TimeBeforeRest, transport.RestDurationFunction, transport.Weather)
    {

    }
}