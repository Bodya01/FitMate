using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Models.Food;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Infrastructure.Mappings
{
    internal sealed class FoodProfile : Profile
    {
        public FoodProfile()
        {
            CreateMap<FoodDto, Food>().ReverseMap();
            CreateMap<CreateFoodModel, Food>();
        }
    }
}