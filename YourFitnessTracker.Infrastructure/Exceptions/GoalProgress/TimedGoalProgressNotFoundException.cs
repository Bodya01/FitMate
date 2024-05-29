namespace YourFitnessTracker.Infrastructure.Exceptions.GoalProgress
{
    public sealed class TimedGoalProgressNotFoundException : EntityNotFoundException
    {
        public TimedGoalProgressNotFoundException(string message) : base(message)
        {
        }
    }
}
