using Race.Interfaces;
using Race.Speed;
using Race.Weather;

namespace Race.Transport.Ground
{
    public class GroundTransport(
        string name,
        ISpeed speed,
        double timeBeforeRest,
        Func<double, double> restDurationFunction, IWeather weather)
        : Transport(name, speed, weather), IGroundTransport
    {
        public double TimeBeforeRest { get; } = timeBeforeRest;
        public Func<double, double> RestDurationFunction { get; } = restDurationFunction;
        public override double CalculateTime(double distance)
        {
            var speed = Speed.GetSpeed(distance) * ApplyWeatherModifier();
            double accumulatedDistance = 0;
            double time = 0;
            var stopCount = 0;

            while (accumulatedDistance < distance)
            {
                if (distance - accumulatedDistance < speed * TimeBeforeRest)
                {
                    time += (distance - accumulatedDistance) / speed;
                    accumulatedDistance = distance; // Полное расстояние пройдено
                }
                else
                {
                    time += TimeBeforeRest;
                    accumulatedDistance += speed * TimeBeforeRest;
                    stopCount++;
                    time += GetRestDuration(stopCount);
                }
            }

            return time;
        }

        public override double ApplyWeatherModifier() => Weather.SpeedModifier(this);

        public double GetRestDuration(int stopNumber) => RestDurationFunction(stopNumber);
    }
}