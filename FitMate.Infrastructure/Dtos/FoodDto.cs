using FitMate.Infrastucture.Dtos.Base;
using FitMate.Infrastucture.Enums;

namespace FitMate.Infrastucture.Dtos;

public record FoodDto(Guid Id, string Name, int Calories, int Carbohydrates, int Protein, int Fat, int ServingSize, ServingUnit ServingUnit, string UserId) : DtoBase;