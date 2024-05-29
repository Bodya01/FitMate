namespace YourFitnessTracker.Infrastructure.Models.Goal.Timed;

public record CreateTimedGoalModel(string Activity, int Hours, int Minutes, int Seconds, float Quantity, string QuantityUnit, string UserId);