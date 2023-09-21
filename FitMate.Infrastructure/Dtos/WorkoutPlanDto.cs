using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos;

public record WorkoutPlanDto(Guid Id, string Name, string SessionsJSON, string UserId) : DtoBase;