namespace Race.Transport.Ground
{
    public class HutOnChickenLegs(GroundTransport transport)
        : GroundTransport(transport.Name, transport.Speed,
            transport.TimeBeforeRest, transport.RestDurationFunction)
    {
    }
}