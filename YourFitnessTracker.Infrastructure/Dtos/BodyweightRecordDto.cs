using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Infrastucture.Dtos;

public record BodyweightRecordDto(Guid Id, DateTime Date, float Weight, string UserId) : DtoBase;