using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos.GoalProgress;

public record GoalProgressDto(Guid Id, DateTime Date, string UserId) : DtoBase;