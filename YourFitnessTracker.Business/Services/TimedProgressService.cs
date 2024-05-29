using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Business.Services.Base;
using YourFitnessTracker.Core.UnitOfWork;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Exceptions.GoalProgress;
using YourFitnessTracker.Infrastructure.Models.GoalProgress.Timed;
using YourFitnessTracker.Infrastucture.Dtos.GoalProgress;

namespace YourFitnessTracker.Business.Services
{
    internal sealed class TimedProgressService : ServiceBase, ITimedProgressService
    {
        public TimedProgressService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task CreateTimedProgressAsync(CreateTimedProgressModel model, CancellationToken cancellationToken = default)
        {
            var goal = await _unitOfWork.TimedGoalRepository.Value.GetByIdAsync(model.GoalId, cancellationToken);

            if (goal is null) throw new TimedGoalProgressNotFoundException($"Goal with id {model.GoalId} does not exist");

            CheckRestrictionsAccess(goal, model.GoalId, model.UserId);

            var entity = _mapper.Map<TimedProgress>(model);
            entity.Time = new TimeSpan(model.Hours, model.Minutes, model.Seconds);

            await _unitOfWork.TimedProgressRepository.Value.CreateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<TimedProgressDto>> GetRecordsForGoalAsync(Guid id, string userId, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.TimedGoalRepository.Value.GetByIdAsync(id, cancellationToken);

            if (entity is null) throw new TimedGoalProgressNotFoundException($"Goal with id {id} does not exist");

            CheckRestrictionsAccess(entity, id, userId);

            var entities = _unitOfWork.TimedProgressRepository.Value.Get(e => e.GoalId == id && e.UserId == userId, s => s)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<TimedProgressDto>>(entities);
        }
    }
}