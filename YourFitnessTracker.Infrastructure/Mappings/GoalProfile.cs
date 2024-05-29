using AutoMapper;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Models.Goal.Timed;
using YourFitnessTracker.Infrastructure.Models.Goal.Weightlifting;
using YourFitnessTracker.Infrastucture.Dtos.Goals;

namespace YourFitnessTracker.Infrastructure.Mappings
{
    internal sealed class GoalProfile : Profile
    {
        public GoalProfile()
        {
            CreateMap<CreateWeightliftingGoalModel, WeightliftingGoal>();
            CreateMap<UpdateWeightliftingGoalModel, WeightliftingGoal>();
            CreateMap<WeightliftingGoalDto, WeightliftingGoal>().ReverseMap();
            CreateMap<CreateTimedGoalModel, TimedGoal>();
            CreateMap<UpdateTimedGoalModel, TimedGoal>();
            CreateMap<TimedGoalDto, TimedGoal>().ReverseMap();
        }
    }
}