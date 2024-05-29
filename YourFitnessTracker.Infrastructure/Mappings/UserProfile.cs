using AutoMapper;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Infrastructure.Mappings
{
    internal sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<FitnessUser, UserDto>().ReverseMap();
        }
    }
}