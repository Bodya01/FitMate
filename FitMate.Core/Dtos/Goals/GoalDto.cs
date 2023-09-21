using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos.Goals;

public record GoalDto(Guid Id, string Activity, UserDto User, ICollection<GoalProgressDto> GoalProgressRecords) : DtoBase;