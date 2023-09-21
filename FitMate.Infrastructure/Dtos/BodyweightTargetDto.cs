namespace FitMate.Infrastucture.Dtos.Base;

public record BodyweightTargetDto(Guid Id, float TargetWeight, DateTime TargetDate, UserDto User) : DtoBase;
