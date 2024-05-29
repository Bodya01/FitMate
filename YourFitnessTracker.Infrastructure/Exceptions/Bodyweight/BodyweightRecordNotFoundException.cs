namespace YourFitnessTracker.Infrastructure.Exceptions.Bodyweight
{
    public sealed class BodyweightRecordNotFoundException : EntityNotFoundException
    {
        public BodyweightRecordNotFoundException(string message) : base(message)
        {
        }
    }
}
