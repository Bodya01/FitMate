namespace YourFitnessTracker.Infrastructure.Exceptions.GoalProgress
{
    public sealed class WeightliftingGoalProgressNotFoundException : EntityNotFoundException
    {
        public WeightliftingGoalProgressNotFoundException(string message) : base(message)
        {
        }
    }
}
