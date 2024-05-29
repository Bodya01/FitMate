using AutoMapper;
using YourFitnessTracker.Business.Interfaces.Base;
using YourFitnessTracker.Core.UnitOfWork;
using YourFitnessTracker.Infrastructure.Entities.Interfaces;
using YourFitnessTracker.Infrastructure.Exceptions;

namespace YourFitnessTracker.Business.Services.Base
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