using YourFitnessTracker.Infrastructure.Entities.Interfaces;

namespace YourFitnessTracker.Infrastructure.Entities;

public class BodyweightRecord : IEntity, IUserOwnedEntity
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }
    public float Weight { get; set; }

    public string UserId { get; set; }
    public FitnessUser User { get; set; }
}