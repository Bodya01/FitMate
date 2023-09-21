namespace FitMate.Infrastucture.Dtos.Goals;

public record WeightliftingGoalDto(Guid Id, string Activity, UserDto User, ICollection<GoalProgressDto> GoalProgressRecords, float Weight, int Reps)
    : GoalDto(Id, Activity, User, GoalProgressRecords);