using FitMate.Infrastructure.Entities.Interfaces;

namespace FitMate.Infrastructure.Entities;

public class WeightliftingGoal : Goal, IEntity
{
    public float Weight { get; set; }
    public int Reps { get; set; }
}