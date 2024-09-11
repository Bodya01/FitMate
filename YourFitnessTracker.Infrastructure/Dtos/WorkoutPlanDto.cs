using System.Text.Json;
using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Infrastucture.Dtos;

public record WorkoutPlanDto(Guid Id, string Name, string SessionsJSON) : DtoBase
{
    public string UserId;
    public ICollection<WorkoutSessionDto> Sessions => string.IsNullOrEmpty(SessionsJSON) ? Array.Empty<WorkoutSessionDto>() : JsonSerializer.Deserialize<WorkoutSessionDto[]>(SessionsJSON);

    public static WorkoutPlanDto CreateDefault() => new(Guid.Empty, "Workout Plan", null);
}