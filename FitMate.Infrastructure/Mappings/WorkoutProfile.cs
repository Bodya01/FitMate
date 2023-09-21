using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Models.WorkoutPlan;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Infrastructure.Mappings
{
    internal sealed class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<WorkoutActivityDto, WorkoutActivity>().ReverseMap();
            CreateMap<WorkoutSessionDto, WorkoutSession>();
            CreateMap<WorkoutSession, WorkoutSessionDto>()
                .ForMember(x => x.WorkoutActivities, opt => opt.MapFrom(x => x.Activities));
            CreateMap<WorkoutPlanDto, WorkoutPlan>().ReverseMap();
            CreateMap<CreateWorkoutPlanModel, WorkoutPlan>();
        }
    }
}