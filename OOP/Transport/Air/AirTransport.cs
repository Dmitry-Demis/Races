using Race.Utility;
using Race.Interfaces;
using Race.Speed;
using Race.Weather;

namespace Race.Transport.Air
{
    public class AirTransport(string name, ISpeed speed, IWeather weather) : Transport(name, speed, weather), IAirTransport
    {
        /// <summary>
        /// Поле для хранения коэффициента ускорения
        /// </summary>
        public double AccelerationCoefficient => GetAcceleration();


        /// <summary>
        /// Метод для получения коэффициента ускорения из скорости
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        private double GetAcceleration()
        {
            if (Speed is LinearSpeed linearSpeed)
                return linearSpeed.Acceleration; // Предполагается, что в LinearSpeed есть свойство Acceleration
            throw new InvalidOperationException("Speed must be of type LinearSpeed to get acceleration.");
        }

        /// <summary>
        /// Метод для вычисления времени полета
        /// </summary>
        /// <param name="distance">Расстояние</param>
        /// <returns></returns>
        public override double CalculateTime(double distance)
        {
            double time = 0;
            double accumulatedDistance = 0;
            var currentSpeed = Speed.GetSpeed(time) * ApplyWeatherModifier(); // Начальная скорость

            // Цикл продолжается, пока пройденное расстояние меньше общего расстояния
            while (accumulatedDistance < distance)
            {
                // Получаем ускорение
                var acceleration = AccelerationCoefficient;

                // Используем формулу s = vt + 0.5 * a * t^2 для нахождения времени
                var dT = 15.0.M(); // Шаг времени
                var dDistance = currentSpeed * dT + 0.5 * acceleration * dT * dT;

                // Если оставшееся расстояние меньше, чем dDistance, корректируем dDistance
                if (accumulatedDistance + dDistance > distance)
                {
                    dDistance = distance - accumulatedDistance; // Оставшееся расстояние
                    dT = Math.Sqrt(2 * dDistance / acceleration); // Вычисляем, сколько времени потребуется для оставшегося расстояния
                }

                // Обновляем пройденное расстояние и время
                accumulatedDistance += dDistance;
                time += dT;

                // Обновляем скорость на следующий шаг
                currentSpeed = Speed.GetSpeed(time);
            }

            return time;
        }
        /// <summary>
        /// Применить погоду
        /// </summary>
        /// <returns>Коэффицент ускорения или замедления</returns>
        public override double ApplyWeatherModifier() => Weather.SpeedModifier(this);
    }
}