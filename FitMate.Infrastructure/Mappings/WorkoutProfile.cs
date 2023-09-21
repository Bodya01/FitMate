using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Infrastructure.Mappings
{
    internal sealed class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<WorkoutActivityDto, WorkoutActivity>().ReverseMap();
            CreateMap<WorkoutSessionDto, WorkoutSession>().ReverseMap();
            CreateMap<WorkoutPlanDto, WorkoutPlan>().ReverseMap();
        }
    }
}