using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Models.BodyweightRecord;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Infrastructure.Mappings
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