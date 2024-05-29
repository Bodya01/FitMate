using AutoMapper;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Models.WorkoutPlan;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Infrastructure.Mappings
{
    internal sealed class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<WorkoutActivityDto, WorkoutActivity>().ReverseMap();
            CreateMap<WorkoutSessionDto, WorkoutSession>();
            CreateMap<WorkoutSession, WorkoutSessionDto>()
                .ForMember(x => x.Activities, opt => opt.MapFrom(x => x.Activities));
            CreateMap<WorkoutPlanDto, WorkoutPlan>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
            CreateMap<WorkoutPlan, WorkoutPlanDto>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnoreAllSourcePropertiesWithAnInaccessibleSetter();

            CreateMap<CreateWorkoutPlanModel, WorkoutPlan>();
            CreateMap<UpdateWorkoutPlanModel, WorkoutPlan>();
        }
    }
}