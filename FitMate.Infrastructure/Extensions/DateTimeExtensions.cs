namespace FitMate.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime EndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month)).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        public static int GetAge(this DateTime date) => DateTime.Now.Subtract(date).Days / 365;
    }
}