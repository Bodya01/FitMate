using System.ComponentModel.DataAnnotations.Schema;
using YourFitnessTracker.Infrastructure.Entities.Interfaces;

namespace YourFitnessTracker.Infrastructure.Entities;

public class TimedProgress : IGoalProgress, IEntity
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }
    public float Quantity { get; set; }
    public TimeSpan Time { get; set; }

    public Guid GoalId { get; set; }
    public string UserId { get; set; }

    public TimedGoal Goal { get; set; }
    public FitnessUser User { get; set; }

    [NotMapped]
    public string QuantityUnit { get { return Goal.QuantityUnit; } }
}
