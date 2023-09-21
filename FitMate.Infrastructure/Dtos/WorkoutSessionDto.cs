using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos;

public record WorkoutSessionDto(string Name, int DayNumber, ICollection<WorkoutActivityDto> WorkoutActivities) : DtoBase;