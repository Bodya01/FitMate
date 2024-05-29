using YourFitnessTracker.Infrastucture.Enums;

namespace YourFitnessTracker.Infrastructure.Models.Food;

public record CreateFoodModel(string Name, int Calories, int Carbohydrates, int Protein, int Fat, int ServingSize, ServingUnit ServingUnit);