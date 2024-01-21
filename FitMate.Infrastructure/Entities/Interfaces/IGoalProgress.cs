namespace FitMate.Infrastructure.Entities.Interfaces;

public interface IGoalProgress : IUserOwnedEntity
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public string UserId { get; set; }

    public FitnessUser User { get; set; }
}