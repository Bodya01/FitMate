using FitMate.Infrastucture.Dtos.Goals;

namespace FitMate.Infrastucture.Dtos.GoalProgress;

public record TimedProgressDto(Guid Id, DateTime Date, string UserId, GoalDto Goal, float Quantity, TimeSpan Time) : GoalProgressDto(Id, Date, UserId, Goal);