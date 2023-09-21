using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos;

public record FoodRecordDto(Guid Id, DateTime ConsumptionDate, float Quantity, FoodDto Food, string UserId) : DtoBase;