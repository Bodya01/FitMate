using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Infrastructure.Mappings
{
    internal sealed class NutritionTargetProfile : Profile
    {
        public NutritionTargetProfile()
        {
            CreateMap<NutritionTargetDto, NutritionTarget>().ReverseMap();
        }
    }
}