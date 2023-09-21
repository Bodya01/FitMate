using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastucture.Dtos.GoalProgress;

namespace FitMate.Infrastructure.Mappings
{
    internal sealed class GoalProgressProfile : Profile
    {
        public GoalProgressProfile()
        {
            CreateMap<GoalProgressDto, GoalProgress>().ReverseMap();
            CreateMap<WeightliftingProgressDto, WeightliftingProgress>().ReverseMap();
            CreateMap<TimedProgressDto, TimedProgress>().ReverseMap();
        }
    }
}