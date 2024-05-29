namespace YourFitnessTracker.Infrastructure.Models.Goal.Weightlifting;

public record CreateWeightliftingGoalModel(string Activity, float Weight, int Reps, string UserId);