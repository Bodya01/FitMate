using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos.Goals;

public abstract record GoalDto(Guid Id, string Activity, string UserId) : DtoBase;