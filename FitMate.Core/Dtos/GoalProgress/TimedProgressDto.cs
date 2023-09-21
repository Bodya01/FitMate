using FitMate.Infrastucture.Dtos.Goals;

namespace FitMate.Infrastucture.Dtos.GoalProgress;

public record TimedProgressDto(Guid Id, DateTime Date, UserDto User, GoalDto Goal, float Quantity, TimeSpan Time) : GoalProgressDto(Id, Date, User, Goal);