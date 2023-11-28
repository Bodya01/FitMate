using FitMate.Infrastucture.Dtos.Base;
using FitMate.Infrastucture.Enums;

namespace FitMate.Infrastucture.Dtos;

public record FoodDto : DtoBase
{
    public Guid Id;
    public string Name;
    public int Calories;
    public int Carbohydrates;
    public int Protein;
    public int Fat;
    public int ServingSize;
    public ServingUnit ServingUnit;
    public string UserId;
    public FoodDto() { }
}