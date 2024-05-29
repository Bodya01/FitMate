namespace YourFitnessTracker.Infrastructure.Exceptions.Food
{
    public sealed class FoodNotFoundException : EntityNotFoundException
    {
        public FoodNotFoundException(string message) : base(message)
        {
        }
    }
}
