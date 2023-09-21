using AutoMapper;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastructure.Mappings
{
    internal sealed class BodyweightTargetProfile : Profile
    {
        public BodyweightTargetProfile()
        {
            CreateMap<BodyweightTargetDto, BodyweightTarget>().ReverseMap();
        }
    }
}