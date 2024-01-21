namespace FitMate.Infrastructure.Models.Goal.Timed;

public record UpdateTimedGoalModel(Guid Id, string Activity, int Hours, int Minutes, int Seconds, float Quantity, string QuantityUnit, string UserId);