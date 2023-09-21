using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos;

public record GoalProgressDto(Guid Id, DateTime Date, UserDto User, GoalDto Goal) : DtoBase;