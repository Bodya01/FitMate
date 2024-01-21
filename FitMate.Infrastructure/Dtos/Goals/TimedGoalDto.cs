using FitMate.Infrastucture.Dtos.GoalProgress;

namespace FitMate.Infrastucture.Dtos.Goals;

public record TimedGoalDto(Guid Id, string Activity, string UserId, ICollection<TimedProgressDto> ProgressRecords, TimeSpan Time, float Quantity, string QuantityUnit)
    : GoalDto(Id, Activity, UserId);