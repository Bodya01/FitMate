namespace YourFitnessTracker.Infrastructure.Exceptions.User
{
    public sealed class UserNotFoundException : EntityNotFoundException
    {
        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}
