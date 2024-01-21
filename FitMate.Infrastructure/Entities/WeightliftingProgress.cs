using FitMate.Infrastructure.Entities.Interfaces;

namespace FitMate.Infrastructure.Entities;

public class WeightliftingProgress : IGoalProgress, IEntity
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }
    public float Weight { get; set; }
    public int Reps { get; set; }

    public string UserId { get; set; }
    public Guid GoalId { get; set; }

    public FitnessUser User { get; set; }
    public WeightliftingGoal Goal { get; set; }
}
