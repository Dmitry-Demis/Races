namespace Race.Utility
{
    public static class TimeExtensions
    {
        public static double H(this double hours) => hours; // Простой метод для часов
        public static double M(this double minutes) => TimeSpan.FromMinutes(minutes).TotalHours; // Перевод минут в часы
    }
}