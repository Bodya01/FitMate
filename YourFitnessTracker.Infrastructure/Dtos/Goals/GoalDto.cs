using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Infrastucture.Dtos.Goals;

public abstract record GoalDto(Guid Id, string Activity, string UserId) : DtoBase;