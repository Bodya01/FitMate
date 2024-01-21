using FitMate.Infrastucture.Dtos.GoalProgress;

namespace FitMate.Infrastucture.Dtos.Goals;

public record WeightliftingGoalDto(Guid Id, string Activity, string UserId, ICollection<WeightliftingProgressDto> ProgressRecords, float Weight, int Reps)
    : GoalDto(Id, Activity, UserId);