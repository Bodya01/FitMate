using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Infrastucture.Dtos.GoalProgress;

public record GoalProgressDto(Guid Id, DateTime Date, string UserId) : DtoBase;