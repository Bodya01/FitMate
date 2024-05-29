using AutoMapper;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Models.BodyweightTarget;
using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Infrastructure.Mappings
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