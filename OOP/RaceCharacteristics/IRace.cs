using Race.Interfaces;

namespace Race.RaceCharacteristics
{
    /// <summary>
    /// Интерфейс для гонки
    /// </summary>
    public interface IRace
    {
        /// <summary>
        /// Регистрирует транспортное средство для участия в гонке.
        /// </summary>
        /// <param name="transport">Транспортное средство, которое необходимо зарегистрировать.</param>
        void RegisterTransport(ITransport transport);

        /// <summary>
        /// Запускает гонку между зарегистрированными транспортными средствами.
        /// </summary>
        void StartRace();

        /// <summary>
        /// Возвращает имя победителя гонки после её завершения.
        /// </summary>
        string? Winner();
    }

}