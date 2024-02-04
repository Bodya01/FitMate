namespace FitMate.Infrastructure.Entities.Interfaces;

public interface IGoalProgress : IUserOwnedEntity
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }
}