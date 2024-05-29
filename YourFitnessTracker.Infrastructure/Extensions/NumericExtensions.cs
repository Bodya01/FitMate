namespace YourFitnessTracker.Infrastructure.Extensions
{
    public static class NumericExtensions
    {
        public static int ToInt(this double value) => (int)Math.Round(value);
    }
}