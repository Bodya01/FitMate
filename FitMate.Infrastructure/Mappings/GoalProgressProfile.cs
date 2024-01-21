using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastucture.Dtos.GoalProgress;

namespace FitMate.Infrastructure.Mappings
{
    internal sealed class GoalProgressProfile : Profile
    {
        public GoalProgressProfile()
        {
            CreateMap<WeightliftingProgress, WeightliftingProgressDto>().ReverseMap();
            CreateMap<TimedProgressDto, TimedProgress>().ReverseMap();
        }
    }
}