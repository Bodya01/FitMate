using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos;
public record WorkoutActivityDto(string Name, string Quantity, int Sets, int RestPeriodSeconds) : DtoBase;