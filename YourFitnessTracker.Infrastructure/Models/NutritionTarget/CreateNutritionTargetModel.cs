namespace YourFitnessTracker.Infrastructure.Models.NutritionTarget;

public record CreateNutritionTargetModel(int Calories, int Carbohydrates, int Proteins, int Fats, string UserId);