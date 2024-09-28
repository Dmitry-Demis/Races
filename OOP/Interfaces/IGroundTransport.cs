namespace Race.Interfaces
{
    /// <summary>
    /// Интерфейс, представляющий наземное транспортное средство.
    /// </summary>
    public interface IGroundTransport : ITransport
    {
        /// <summary>
        /// Время, которое транспортное средство может ехать без остановки (в часах).
        /// </summary>
        double TimeBeforeRest { get; }

        /// <summary>
        /// Рассчитывает продолжительность отдыха транспортного средства, которая зависит от порядкового номера остановки
        /// </summary>
        /// <param name="stopNumber">Номер остановки (например, первая, вторая, и т.д.).</param>
        /// <returns>Продолжительность отдыха в часах.</returns>
        double GetRestDuration(int stopNumber);
    }

}