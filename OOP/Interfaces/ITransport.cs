using Race.Speed;
using Race.Weather;

namespace Race.Interfaces
{
    /// <summary>
    /// Интерфейс, представляющий транспортное средство.
    /// </summary>
    public interface ITransport
    {
        /// <summary>
        /// Название транспортного средства.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Объект, представляющий закон скорости транспортного средства.
        /// </summary>
        ISpeed Speed { get; }

        /// <summary>
        /// Рассчитывает время, необходимое для преодоления заданного расстояния.
        /// </summary>
        /// <param name="distance">Расстояние, которое нужно преодолеть (в километрах).</param>
        /// <returns>Время в часах, необходимое для преодоления расстояния.</returns>
        double CalculateTime(double distance);

        /// <summary>
        /// Погодное условие, которое влияет на транспортное средство.
        /// </summary>
        IWeather Weather { get; }

        /// <summary>
        /// Применяет модификатор скорости в зависимости от текущей погоды.
        /// </summary>
        /// <returns>Модифицированная скорость с учётом погодных условий.</returns>
        double ApplyWeatherModifier();
    }

}