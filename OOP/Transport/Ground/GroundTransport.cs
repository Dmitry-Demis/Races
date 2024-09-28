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
        /// <summary>
        /// Рассчитывает общее время, необходимое для прохождения заданного расстояния.
        /// Учитываются скорость транспорта, модификатор скорости в зависимости от погоды, 
        /// а также периоды отдыха для наземных транспортных средств.
        /// </summary>
        /// <param name="distance">Общее расстояние, которое должен преодолеть транспорт.</param>
        /// <returns>Время, необходимое для преодоления указанного расстояния.</returns>
        public override double CalculateTime(double distance)
        {
            // Получаем текущую скорость с учётом погоды.
            var speed = Speed.GetSpeed(distance) * ApplyWeatherModifier();

            // Счётчики пройденного расстояния и общего времени.
            double accumulatedDistance = 0;
            double time = 0;

            // Счётчик остановок для наземных транспортных средств.
            var stopCount = 0;

            // Пока транспорт не пройдет всё расстояние.
            while (accumulatedDistance < distance)
            {
                // Если оставшееся расстояние меньше, чем можно преодолеть до следующей остановки.
                if (distance - accumulatedDistance < speed * TimeBeforeRest)
                {
                    // Добавляем время для преодоления оставшегося расстояния.
                    time += (distance - accumulatedDistance) / speed;

                    // Обновляем пройденное расстояние до общего.
                    accumulatedDistance = distance;
                }
                else
                {
                    // Добавляем время до следующей остановки.
                    time += TimeBeforeRest;

                    // Обновляем пройденное расстояние.
                    accumulatedDistance += speed * TimeBeforeRest;

                    // Увеличиваем счётчик остановок.
                    stopCount++;

                    // Добавляем время на отдых, зависящее от номера остановки.
                    time += GetRestDuration(stopCount);
                }
            }

            // Возвращаем общее время.
            return time;
        }


        public override double ApplyWeatherModifier() => Weather.SpeedModifier(this);

        public double GetRestDuration(int stopNumber) => RestDurationFunction(stopNumber);
    }
}