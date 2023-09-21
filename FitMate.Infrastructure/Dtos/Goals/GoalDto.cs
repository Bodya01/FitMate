using FitMate.Infrastucture.Dtos.Base;
using FitMate.Infrastucture.Dtos.GoalProgress;

namespace FitMate.Infrastucture.Dtos.Goals;

public record GoalDto(Guid Id, string Activity, string UserId, ICollection<GoalProgressDto> GoalProgressRecords) : DtoBase;