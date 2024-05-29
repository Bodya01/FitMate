namespace YourFitnessTracker.Infrastructure.Exceptions.Workout
{
    public sealed class WorkoutPlanNotFoundException : EntityNotFoundException
    {
        public WorkoutPlanNotFoundException(string message) : base(message)
        {
        }
    }
}
