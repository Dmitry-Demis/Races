using Race.Interfaces;
using Race.Speed;

namespace Race.Transport.Ground
{
    public class GroundTransport(
        string name,
        ISpeed speed,
        double timeBeforeRest,
        Func<double, double> restDurationFunction)
        : Transport(name, speed), IGroundTransport
    {
        public double TimeBeforeRest { get; } = timeBeforeRest;
        public Func<double, double> RestDurationFunction { get; } = restDurationFunction;
        public override double CalculateTime(double distance)
        {
            var speed = Speed.GetSpeed(distance);
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

        public double GetRestDuration(int stopNumber) => RestDurationFunction(stopNumber);
    }
}