namespace FitMate.Infrastructure.Models.GoalProgress.Weightlifting;

public record CreateWeightliftingProgressModel(DateTime Date, float Weight, int Reps, Guid GoalId, string UserId);