using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Infrastucture.Dtos;

public record FoodRecordDto(Guid Id, DateTime ConsumptionDate, float Quantity, string UserId) : DtoBase
{
    public FoodDto Food;
}