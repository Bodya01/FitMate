using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Models.BodyweightTarget;
using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastructure.Mappings
{
    internal sealed class BodyweightTargetProfile : Profile
    {
        public BodyweightTargetProfile()
        {
            CreateMap<BodyweightTargetDto, BodyweightTarget>().ReverseMap();
            CreateMap<UpdateBodyweightTargetModel, BodyweightTarget>()
                .ForMember(x => x.TargetDate, opt => opt.MapFrom(x => x.Date))
                .ForMember(x => x.TargetWeight, opt => opt.MapFrom(x => x.Weight));
        }
    }
}