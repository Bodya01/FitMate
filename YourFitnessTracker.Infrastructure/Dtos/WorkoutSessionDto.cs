using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Infrastucture.Dtos;

public record WorkoutSessionDto(string Name, int DayNumber) : DtoBase
{
    public ICollection<WorkoutActivityDto> Activities { get; set; }
}