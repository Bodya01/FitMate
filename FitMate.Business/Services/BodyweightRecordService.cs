using AutoMapper;
using FitMate.Business.Services.Base;
using FitMate.Core.UnitOfWork;

namespace FitMate.Business.Services
{
    internal sealed class BodyweightRecordService : ServiceBase//, IBodyweightRecordService
    {
        public BodyweightRecordService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}