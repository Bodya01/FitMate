using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Models.Goal.Timed;
using FitMate.Infrastructure.Models.Goal.Weightlifting;
using FitMate.Infrastucture.Dtos.Goals;

namespace FitMate.Infrastructure.Mappings
{
    internal sealed class GoalProfile : Profile
    {
        public GoalProfile()
        {
            CreateMap<CreateWeightliftingGoalModel, WeightliftingGoal>();
            CreateMap<WeightliftingGoalDto, WeightliftingGoal>().ReverseMap();
            CreateMap<CreateTimedGoalModel, TimedGoal>();
            CreateMap<TimedGoalDto, TimedGoal>().ReverseMap();
        }
    }
}