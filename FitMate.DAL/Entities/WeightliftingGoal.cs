namespace FitMate.DAL.Entities;

public class WeightliftingGoal : Goal, IEntity
{
    public float Weight { get; set; }
    public int Reps { get; set; }
}
