using YourFitnessTracker.Infrastucture.Dtos.Base;
using System.Text.Json;

namespace YourFitnessTracker.Infrastucture.Dtos;

public record WorkoutPlanDto(Guid Id, string Name, string SessionsJSON) : DtoBase
{
    public string UserId;
    public ICollection<WorkoutSessionDto> Sessions => string.IsNullOrEmpty(SessionsJSON) ? new WorkoutSessionDto[0] : JsonSerializer.Deserialize<WorkoutSessionDto[]>(SessionsJSON);
}