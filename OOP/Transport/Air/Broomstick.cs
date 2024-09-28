
namespace Race.Transport.Air
{
    public sealed class Broomstick(AirTransport transport)
        : AirTransport(transport.Name, transport.Speed, transport.Weather)
    {
        // Можно добавить уникальные свойства или методы для Broomstick, если это необходимо
    }
}