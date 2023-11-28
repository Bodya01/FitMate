using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos;

public record WorkoutSessionDto(string Name, int DayNumber) : DtoBase
{
    public ICollection<WorkoutActivityDto> Activities { get; set; }
}