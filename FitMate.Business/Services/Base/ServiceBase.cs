using AutoMapper;
using FitMate.Business.Interfaces.Base;
using FitMate.Core.UnitOfWork;

namespace FitMate.Business.Services.Base
{
    public class ServiceBase : IServiceBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public ServiceBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}