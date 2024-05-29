namespace YourFitnessTracker.Infrastructure.Exceptions.Goal
{
    public sealed class WeightliftingGoalNotFoundException : EntityNotFoundException
    {
        public WeightliftingGoalNotFoundException(string message) : base(message)
        {
        }
    }
}
