using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Models.NutritionTarget;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Infrastructure.Mappings
{
    internal sealed class NutritionTargetProfile : Profile
    {
        public NutritionTargetProfile()
        {
            CreateMap<NutritionTargetDto, NutritionTarget>().ReverseMap();

            CreateMap<CreateNutritionTargetModel, NutritionTarget>()
                .ForMember(x => x.DailyProtein, opt => opt.MapFrom(x => x.Proteins))
                .ForMember(x => x.DailyCarbohydrates, opt => opt.MapFrom(x => x.Carbohydrates))
                .ForMember(x => x.DailyFat, opt => opt.MapFrom(x => x.Fats))
                .ForMember(x => x.DailyCalories, opt => opt.MapFrom(x => x.Calories));
        }
    }
}