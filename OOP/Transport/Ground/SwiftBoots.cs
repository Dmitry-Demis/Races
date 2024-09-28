
namespace Race.Transport.Ground
{
    public class SwiftBoots(GroundTransport transport)
        : GroundTransport(transport.Name, transport.Speed,
            transport.TimeBeforeRest, transport.RestDurationFunction, transport.Weather)
    {
    }
}