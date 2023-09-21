using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Infrastructure.Mappings
{
    internal sealed class FoodRecordProfile : Profile
    {
        public FoodRecordProfile()
        {
            CreateMap<FoodRecordDto, FoodRecord>().ReverseMap();
        }
    }
}