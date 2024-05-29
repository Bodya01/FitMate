namespace YourFitnessTracker.Infrastructure.Exceptions.Bodyweight
{
    public sealed class BodyweightTargetNotFoundException : EntityNotFoundException
    {
        public BodyweightTargetNotFoundException(string message) : base(message)
        {
        }
    }
}