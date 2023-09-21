using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastucture.Dtos.Goals;

namespace FitMate.Infrastructure.Mappings
{
    internal sealed class GoalProfile : Profile
    {
        public GoalProfile()
        {
            CreateMap<GoalDto, Goal>().ReverseMap();
            CreateMap<WeightliftingGoalDto, WeightliftingGoal>().ReverseMap();
            CreateMap<TimedGoalDto, TimedGoal>().ReverseMap();
        }
    }
}