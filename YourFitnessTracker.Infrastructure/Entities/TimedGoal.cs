using YourFitnessTracker.Infrastructure.Entities.Interfaces;

namespace YourFitnessTracker.Infrastructure.Entities;

public class TimedGoal : IGoal, IEntity
{
    public Guid Id { get; set; }

    public string Activity { get; set; }
    public TimeSpan Time { get; set; }
    public float Quantity { get; set; }
    public string QuantityUnit { get; set; }

    public string UserId { get; set; }

    public FitnessUser User { get; set; }
    public ICollection<TimedProgress> ProgressRecords { get; set; }
}