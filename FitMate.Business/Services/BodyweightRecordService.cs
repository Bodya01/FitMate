using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Business.Services.Base;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Models.WorkoutPlan;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Business.Services
{
    public sealed class BodyweightRecordService : ServiceBase//, IBodyweightRecordService
    {
        public BodyweightRecordService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}