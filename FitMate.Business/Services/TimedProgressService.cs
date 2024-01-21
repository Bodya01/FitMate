using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Business.Services.Base;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Exceptions;
using FitMate.Infrastructure.Models.GoalProgress.Timed;

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
    }
}