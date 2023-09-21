using FitMate.Infrastucture.Dtos.GoalProgress;

namespace FitMate.Infrastucture.Dtos.Goals;

public record TimedGoalDto(Guid Id, string Activity, UserDto User, ICollection<GoalProgressDto> GoalProgressRecords, TimeSpan Time, float Quantity, string QuantityUnit)
    : GoalDto(Id, Activity, User, GoalProgressRecords);