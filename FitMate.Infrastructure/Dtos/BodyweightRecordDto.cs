using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos;

public record BodyweightRecordDto(Guid Id, DateTime Date, float Weight, UserDto User) : DtoBase;