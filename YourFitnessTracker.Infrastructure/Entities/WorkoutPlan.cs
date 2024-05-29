using YourFitnessTracker.Infrastructure.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace YourFitnessTracker.Infrastructure.Entities;

public class WorkoutPlan : IEntity, IAuditedEntity, IUserOwnedEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    // TODO: Replace with actual entities
    public string SessionsJSON { get; set; }
    public DateTime CreatedAt { get; set; }

    public string UserId { get; set; }
    public FitnessUser User { get; set; }

    [NotMapped]
    public ICollection<WorkoutSession> Sessions
    {
        get => string.IsNullOrEmpty(SessionsJSON) ? new WorkoutSession[0] : JsonSerializer.Deserialize<WorkoutSession[]>(SessionsJSON);
    }
}