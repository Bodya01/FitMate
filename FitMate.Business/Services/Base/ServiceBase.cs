using AutoMapper;
using FitMate.Business.Interfaces.Base;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities.Interfaces;
using FitMate.Infrastructure.Exceptions;

namespace FitMate.Business.Services.Base
{
    public abstract class ServiceBase : IServiceBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public ServiceBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        protected virtual void CheckRestrictionsAccess<T>(T entity, Guid id, string userId) where T : IUserOwnedEntity
        {
            if (entity.UserId != userId) throw new ForbiddenException($"User with {userId} id has no access to {entity.GetType().Name} with {id} id");
        }
    }
}