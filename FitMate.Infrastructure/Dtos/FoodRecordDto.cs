using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos;

public record FoodRecordDto(Guid Id, DateTime ConsumptionDate, float Quantity, string UserId) : DtoBase
{
    public FoodDto Food;
}