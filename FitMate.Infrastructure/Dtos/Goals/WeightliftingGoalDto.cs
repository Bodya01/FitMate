using FitMate.Infrastucture.Dtos.GoalProgress;

namespace FitMate.Infrastucture.Dtos.Goals;

public record WeightliftingGoalDto(Guid Id, string Activity, string UserId, ICollection<GoalProgressDto> GoalProgressRecords, float Weight, int Reps)
    : GoalDto(Id, Activity, UserId, GoalProgressRecords);