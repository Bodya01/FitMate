using FitMate.Infrastucture.Dtos.Goals;

namespace FitMate.Infrastucture.Dtos.GoalProgress;

public record TimedProgressDto(Guid Id, DateTime Date, string UserId, TimedGoalDto Goal, float Quantity, TimeSpan Time, string QuantityUnit) : GoalProgressDto(Id, Date, UserId);