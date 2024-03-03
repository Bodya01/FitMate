using FitMate.Infrastructure.Enums;
using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos;

public record UserDto(string Id, string FirstName, string LastName, string Email, DateTime DateOfBirth, Genders Gender, float? Height) : DtoBase;