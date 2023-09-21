using FitMate.Infrastucture.Dtos.GoalProgress;

namespace FitMate.Infrastucture.Dtos.Goals;

public record TimedGoalDto(Guid Id, string Activity, string UserId, ICollection<GoalProgressDto> GoalProgressRecords, TimeSpan Time, float Quantity, string QuantityUnit)
    : GoalDto(Id, Activity, UserId, GoalProgressRecords);