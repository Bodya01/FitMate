namespace YourFitnessTracker.Infrastucture.Dtos.GoalProgress;

public record TimedProgressDto(Guid Id, DateTime Date, string UserId, float Quantity, TimeSpan Time, string QuantityUnit) : GoalProgressDto(Id, Date, UserId);