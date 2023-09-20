namespace FitMate.Infrastructure.Entities;

public class WeightliftingProgress : GoalProgress, IEntity
{
    public float Weight { get; set; }
    public int Reps { get; set; }
}
