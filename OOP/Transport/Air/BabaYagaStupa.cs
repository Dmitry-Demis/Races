using Race.Transport.Air;

namespace Race
{
    public class BabaYagaStupa(AirTransport transport)
        : AirTransport(transport.Name, transport.Speed, transport.Weather)
    {
        // Можно добавить уникальные свойства или методы для BabaYagaStupa, если это необходимо
    }
}