using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Business.Services.Base;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Exceptions;
using FitMate.Infrastructure.Models.GoalProgress.Weightlifting;

namespace FitMate.Business.Services
{
    internal sealed class WeightliftingProgressService : ServiceBase, IWeightliftingProgressService
    {
        public WeightliftingProgressService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task CreateWeightliftingProgressAsync(CreateWeightliftingProgressModel model, CancellationToken cancellationToken = default)
        {
            var goal = await _unitOfWork.WeightliftingGoalRepository.Value.GetByIdAsync(model.GoalId, cancellationToken);

            if (goal is null) throw new EntityNotFoundException($"Goal with id {model.GoalId} does not exist");

            CheckRestrictionsAccess(goal, model.GoalId, model.UserId);

            var entity = _mapper.Map<WeightliftingProgress>(model);
            await _unitOfWork.WeightliftingProgressRepository.Value.CreateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}