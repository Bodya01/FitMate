using FitMate.Infrastucture.Enums;

namespace FitMate.Infrastructure.Models.Food;

public record CreateFoodModel(string Name, int Calories, int Carbohydrates, int Protein, int Fat, int ServingSize, ServingUnit ServingUnit);