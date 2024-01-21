namespace FitMate.Infrastructure.Models.Goal.Weightlifting;

public record UpdateWeightliftingGoalModel(Guid Id, string Activity, float Weight, int Reps, string UserId);