using AutoMapper;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Models.BodyweightRecord;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Infrastructure.Mappings
{
    internal sealed class BodyweightRecordProfile : Profile
    {
        public BodyweightRecordProfile()
        {
            CreateMap<BodyweightRecord, BodyweightRecordDto>().ReverseMap();

            CreateMap<CreateTodayBodyweightRecordModel, BodyweightRecord>();
            CreateMap<UpdateBodyweightRecordModel, BodyweightRecord>();
        }
    }
}