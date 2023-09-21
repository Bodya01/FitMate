using FitMate.Infrastucture.Dtos.Base;
using FitMate.Infrastucture.Dtos.Goals;

namespace FitMate.Infrastucture.Dtos.GoalProgress;

public record GoalProgressDto(Guid Id, DateTime Date, string UserId, GoalDto Goal) : DtoBase;