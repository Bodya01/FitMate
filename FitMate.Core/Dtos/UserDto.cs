using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos;

public record UserDto(string Id, string FirstName, string LastName, string Email, DateTime DateOfBirth, string Gender) : DtoBase;