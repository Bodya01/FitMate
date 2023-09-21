using FitMate.Infrastructure.Entities.Interfaces;

namespace FitMate.Infrastructure.Entities;

public class BodyweightTarget : IEntity
{
    public Guid Id { get; set; }

    public float TargetWeight { get; set; }
    public DateTime TargetDate { get; set; }

    public string UserId { get; set; }
    public FitnessUser User { get; set; }
}