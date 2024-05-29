namespace YourFitnessTracker.Infrastructure.Models.GoalProgress.Timed;

public record CreateTimedProgressModel(DateTime Date, float Quantity, int Hours, int Minutes, int Seconds, Guid GoalId, string UserId);