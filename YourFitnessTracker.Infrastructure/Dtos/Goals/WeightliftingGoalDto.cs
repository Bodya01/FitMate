using YourFitnessTracker.Infrastucture.Dtos.GoalProgress;

namespace YourFitnessTracker.Infrastucture.Dtos.Goals;

public record WeightliftingGoalDto(Guid Id, string Activity, string UserId, ICollection<WeightliftingProgressDto> ProgressRecords, float Weight, int Reps)
    : GoalDto(Id, Activity, UserId)
{
    public static WeightliftingGoalDto CreateDefault() =>
        new(Guid.Empty, string.Empty, string.Empty, null, default, default);
}