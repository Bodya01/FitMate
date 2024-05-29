using YourFitnessTracker.Infrastructure.Enums;
using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Infrastucture.Dtos;

public record UserDto(string Id, string FirstName, string LastName, string Email, DateTime DateOfBirth, Genders Gender, float? Height) : DtoBase;