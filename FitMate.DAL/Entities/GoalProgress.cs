namespace FitMate.DAL.Entities;

public class GoalProgress : IEntity
{
    public long Id { get; set; }

    public DateTime Date { get; set; }

    public string UserId { get; set; }
    public long GoalId { get; set; }
    
    public FitnessUser User { get; set; }
    public Goal Goal { get; set; }
}