namespace FitMate.Infrastructure.Entities;

public class BodyweightRecord : IEntity
{
    public long Id { get; set; }

    public DateTime Date { get; set; }
    public float Weight { get; set; }

    public string UserId { get; set; }
    public FitnessUser User { get; set; }
}