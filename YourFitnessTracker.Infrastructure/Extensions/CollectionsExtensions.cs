namespace YourFitnessTracker.Infrastructure.Extensions
{
    public static class CollectionsExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection is null || !collection.Any();
    }
}