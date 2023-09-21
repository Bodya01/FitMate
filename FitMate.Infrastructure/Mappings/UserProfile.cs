using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Infrastructure.Mappings
{
    internal sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<FitnessUser, UserDto>().ReverseMap();
        }
    }
}
