using YourFitnessTracker.Infrastructure.Entities.Interfaces;

namespace YourFitnessTracker.Infrastructure.Entities;

public class BodyweightTarget : IEntity, IUserOwnedEntity, IAuditedEntity
{
    public Guid Id { get; set; }

    public float TargetWeight { get; set; }
    public DateTime TargetDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public string UserId { get; set; }
    public FitnessUser User { get; set; }
}