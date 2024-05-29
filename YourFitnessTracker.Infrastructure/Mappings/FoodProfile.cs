using AutoMapper;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Models.Food;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Infrastructure.Mappings
{
    internal sealed class FoodProfile : Profile
    {
        public FoodProfile()
        {
            CreateMap<FoodDto, Food>().ReverseMap();
            CreateMap<CreateFoodModel, Food>();
            CreateMap<UpdateFoodModel, Food>();
        }
    }
}