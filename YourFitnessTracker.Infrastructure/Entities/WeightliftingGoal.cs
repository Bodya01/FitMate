using YourFitnessTracker.Infrastructure.Entities.Interfaces;

namespace YourFitnessTracker.Infrastructure.Entities;

public class WeightliftingGoal : IGoal, IEntity
{
    public Guid Id { get; set; }

    public string Activity { get; set; }
    public float Weight { get; set; }
    public int Reps { get; set; }

    public string UserId { get; set; }

    public FitnessUser User { get; set; }
    public ICollection<WeightliftingProgress> ProgressRecords { get; set; }
}