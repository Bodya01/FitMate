using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos;

public record WorkoutPlanDto(Guid Id, string Name, string SessionsJSON, ICollection<WorkoutSessionDto> WorkoutSessions) : DtoBase;