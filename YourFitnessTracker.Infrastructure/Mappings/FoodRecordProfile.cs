using AutoMapper;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Models.FoodRecord;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Infrastructure.Mappings
{
    internal sealed class FoodRecordProfile : Profile
    {
        public FoodRecordProfile()
        {
            CreateMap<FoodRecordDto, FoodRecord>().ReverseMap();
            CreateMap<CreateFoodRecordModel, FoodRecord>();
        }
    }
}