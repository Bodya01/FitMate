namespace FitMate.Infrastructure.Entities.Interfaces
{
    public interface IUserOwnedEntity
    {
        public string UserId { get; set; }
        public FitnessUser User { get; set; }
    }
}