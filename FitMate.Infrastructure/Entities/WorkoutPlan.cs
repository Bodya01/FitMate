using FitMate.Infrastructure.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace FitMate.Infrastructure.Entities;

public class WorkoutPlan : IEntity, IAuditedEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }
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