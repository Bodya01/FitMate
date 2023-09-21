using FitMate.Infrastucture.Dtos.Goals;

namespace FitMate.Infrastucture.Dtos.GoalProgress;

public record WeightliftingProgressDto(Guid Id, DateTime Date, UserDto User, GoalDto Goal, float Weight, int Reps) : GoalProgressDto(Id, Date, User, Goal);