namespace FitMate.Infrastructure.Entities;

public class Goal : IEntity
{
    public Guid Id { get; set; }

    public string Activity { get; set; }

    public string UserId { get; set; }

    public FitnessUser User { get; set; }
    public ICollection<GoalProgress> GoalProgressRecords { get; set; }
}
