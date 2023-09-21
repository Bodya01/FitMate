namespace FitMate.Infrastructure.Models.WorkoutPlan;

public class CreateWorkoutPlanModel
{
    public string Name { get; set; }
    public string SessionsJson { get; set; }
    public string UserId { get; set; }
}