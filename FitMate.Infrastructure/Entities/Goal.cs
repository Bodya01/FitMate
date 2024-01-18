using FitMate.Infrastructure.Entities.Interfaces;

namespace FitMate.Infrastructure.Entities;

// TODO: Review use of TPH and Discriminator
public class Goal : IEntity, IUserOwnedEntity
{
    public Guid Id { get; set; }

    public string Activity { get; set; }

    public string UserId { get; set; }

    public FitnessUser User { get; set; }
    public ICollection<GoalProgress> GoalProgressRecords { get; set; }
}