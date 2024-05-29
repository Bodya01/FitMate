namespace YourFitnessTracker.Infrastructure.Exceptions.Goal
{
    public sealed class TimedGoalNotFoundException : EntityNotFoundException
    {
        public TimedGoalNotFoundException(string message) : base(message) { }
    }
}