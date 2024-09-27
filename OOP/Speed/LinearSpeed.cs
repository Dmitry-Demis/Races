namespace Race.Speed
{
    public class LinearSpeed(double initialSpeed, double acceleration) : Speed
    {
        public double InitialSpeed { get; } = initialSpeed;
        public double Acceleration { get; } = acceleration;

        // Реализация метода для получения скорости в зависимости от времени
        public override double GetSpeed(double time) =>
            // Возвращаем скорость на основе начальной скорости и ускорения
            InitialSpeed + Acceleration * time;
    }
}