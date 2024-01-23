using FitMate.Infrastucture.Enums;

namespace FitMate.Infrastructure.Models.Food;

public record UpdateFoodModel(Guid Id, string Name, int Calories, int Carbohydrates, int Protein, int Fat, int ServingSize, ServingUnit ServingUnit);