using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Business.Services.Base;
using YourFitnessTracker.Core.UnitOfWork;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Exceptions;
using YourFitnessTracker.Infrastructure.Exceptions.Goal;
using YourFitnessTracker.Infrastructure.Extensions;
using YourFitnessTracker.Infrastructure.Models.Goal.Timed;
using YourFitnessTracker.Infrastucture.Dtos.Goals;

namespace YourFitnessTracker.Business.Services
{
    internal sealed class TimedGoalService : ServiceBase, ITimedGoalService
    {
        public TimedGoalService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<TimedGoalDto> GetTimedGoal(Guid id, string userId, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.TimedGoalRepository.Value.GetByIdAsync(id, cancellationToken);

            if (entity is null) throw new TimedGoalNotFoundException($"Timed goal with id {id} does not exist");

            CheckRestrictionsAccess(entity, id, userId);

            await _unitOfWork.TimedGoalRepository.Value.LoadNavigationCollectionExplicitly(entity, x => x.ProgressRecords, cancellationToken);

            return _mapper.Map<TimedGoalDto>(entity);
        }

        public async Task<IEnumerable<TimedGoalDto>> GetTimedGoalsForUser(string userId, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.TimedGoalRepository.Value.Get(e => e.UserId == userId, s => s)
                .ToListAsync(cancellationToken);

            if (entities.IsNullOrEmpty()) throw new TimedGoalNotFoundException($"No weightlifting goals found for user {userId}");

            return _mapper.Map<IEnumerable<TimedGoalDto>>(entities);
        }

        public async Task CreateTimedGoalAsync(CreateTimedGoalModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<TimedGoal>(model);

            entity.Time = new TimeSpan(model.Hours, model.Minutes, model.Seconds);

            await _unitOfWork.TimedGoalRepository.Value.CreateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateTimedGoalAsync(UpdateTimedGoalModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.TimedGoalRepository.Value.GetByIdAsync(model.Id, cancellationToken);

            if (entity is null) throw new TimedGoalNotFoundException($"Timed goal with id {model.Id} does not exist");

            CheckRestrictionsAccess(entity, model.Id, model.UserId ?? entity.UserId);

            _mapper.Map(model, entity);
            entity.Time = new TimeSpan(model.Hours, model.Minutes, model.Seconds);

            await _unitOfWork.TimedGoalRepository.Value.UpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}