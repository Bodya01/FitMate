using AutoMapper;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Models.GoalProgress.Timed;
using YourFitnessTracker.Infrastructure.Models.GoalProgress.Weightlifting;
using YourFitnessTracker.Infrastucture.Dtos.GoalProgress;

namespace YourFitnessTracker.Infrastructure.Mappings
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