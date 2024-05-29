using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Infrastucture.Dtos;
public record WorkoutActivityDto(string Name, string Quantity, int Sets, int RestPeriodSeconds) : DtoBase;