namespace FitMate.DAL.Entities;

public class TimedGoal : Goal, IEntity
{
    public TimeSpan Time { get; set; }
    public float Quantity { get; set; }
    public string QuantityUnit { get; set; }
}
