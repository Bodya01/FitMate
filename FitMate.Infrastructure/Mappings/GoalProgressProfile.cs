using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Models.GoalProgress.Timed;
using FitMate.Infrastructure.Models.GoalProgress.Weightlifting;
using FitMate.Infrastucture.Dtos.GoalProgress;

namespace FitMate.Infrastructure.Mappings
{
    internal sealed class GoalProgressProfile : Profile
    {
        public GoalProgressProfile()
        {
            CreateMap<CreateWeightliftingProgressModel, WeightliftingProgress>();
            CreateMap<WeightliftingProgressDto, WeightliftingProgress>().ReverseMap();
            CreateMap<CreateTimedProgressModel, TimedProgress>();
            CreateMap<TimedProgressDto, TimedProgress>().ReverseMap();
        }
    }
}