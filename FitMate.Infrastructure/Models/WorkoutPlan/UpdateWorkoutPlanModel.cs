namespace FitMate.Infrastructure.Models.WorkoutPlan;

public class UpdateWorkoutPlanModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SessionsJson { get; set; }
    public string UserId { get; set; }
}