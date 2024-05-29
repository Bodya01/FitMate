namespace YourFitnessTracker.Infrastucture.Dtos.GoalProgress;

public record WeightliftingProgressDto(Guid Id, DateTime Date, string UserId, float Weight, int Reps) : GoalProgressDto(Id, Date, UserId);