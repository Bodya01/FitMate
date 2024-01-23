namespace FitMate.Infrastructure.Models.WorkoutPlan;

public record UpdateWorkoutPlanModel(Guid Id, string Name, string SessionsJson, string UserId);