using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Business.Services.Base;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Exceptions;
using FitMate.Infrastructure.Models.GoalProgress.Timed;
using FitMate.Infrastucture.Dtos.GoalProgress;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Business.Services
{
    internal sealed class TimedProgressService : ServiceBase, ITimedProgressService
    {
        public TimedProgressService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task CreateTimedProgressAsync(CreateTimedProgressModel model, CancellationToken cancellationToken = default)
        {
            var goal = await _unitOfWork.TimedGoalRepository.Value.GetByIdAsync(model.GoalId, cancellationToken);

            if (goal is null) throw new EntityNotFoundException($"Goal with id {model.GoalId} does not exist");

            CheckRestrictionsAccess(goal, model.GoalId, model.UserId);

            var entity = _mapper.Map<TimedProgress>(model);
            entity.Time = new TimeSpan(model.Hours, model.Minutes, model.Seconds);

            await _unitOfWork.TimedProgressRepository.Value.CreateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<TimedProgressDto>> GetRecordsForGoalAsync(Guid id, string userId, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.TimedGoalRepository.Value.GetByIdAsync(id, cancellationToken);

            if (entity is null) throw new EntityNotFoundException($"Goal with id {id} does not exist");

            CheckRestrictionsAccess(entity, id, userId);

            var entities = _unitOfWork.TimedProgressRepository.Value.Get(e => e.GoalId == id && e.UserId == userId, s => s)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<TimedProgressDto>>(entities);
        }
    }
}