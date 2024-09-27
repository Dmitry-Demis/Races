
namespace Race.Transport.Air
{
    public class Broomstick(AirTransport transport)
        : AirTransport(transport.Name, transport.Speed)
    {
        // Можно добавить уникальные свойства или методы для Broomstick, если это необходимо
    }
}