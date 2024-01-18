using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Business.Services.Base;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Exceptions;
using FitMate.Infrastructure.Extensions;
using FitMate.Infrastucture.Dtos.Goals;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Business.Services
{
    internal sealed class TimedGoalService : ServiceBase, ITimedGoalService
    {
        public TimedGoalService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<IEnumerable<TimedGoalDto>> GetTimedGoalsForUser(string userId, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.TimedGoalRepository.Value.Get(e => e.UserId == userId, s => s)
                .ToListAsync(cancellationToken);

            if (entities.IsNullOrEmpty()) throw new EntityNotFoundException($"No weightlifting goals found for user {userId}");

            return _mapper.Map<IEnumerable<TimedGoalDto>>(entities);
        }
    }
}