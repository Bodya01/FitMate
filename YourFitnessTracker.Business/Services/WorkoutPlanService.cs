using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Business.Services.Base;
using YourFitnessTracker.Core.UnitOfWork;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Exceptions;
using YourFitnessTracker.Infrastructure.Models.WorkoutPlan;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Business.Services
{
    internal sealed class WorkoutPlanService : ServiceBase, IWorkoutPlanService
    {
        public WorkoutPlanService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<WorkoutPlanDto> GetWorkoutAsync(Guid id, string userId, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.WorkoutPlanRepository.Value.GetByIdAsync(id, cancellationToken);

            if (entity is null) throw new EntityNotFoundException($"Workout plan with {id} id was not found");

            CheckRestrictionsAccess(entity, id, userId);

            return _mapper.Map<WorkoutPlanDto>(entity);
        }

        public async Task<IEnumerable<WorkoutPlanDto>> GetWorkoutsAsync(string userId, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.WorkoutPlanRepository.Value.Get(w => w.UserId == userId, s => s)
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync(cancellationToken);

            entities ??= new();

            return _mapper.Map<IEnumerable<WorkoutPlanDto>>(entities);
        }

        public async Task CreateWorkoutPlanAsync(CreateWorkoutPlanModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<WorkoutPlan>(model);

            await _unitOfWork.WorkoutPlanRepository.Value.CreateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateWorkoutPlanAsync(UpdateWorkoutPlanModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.WorkoutPlanRepository.Value.GetByIdAsync(model.Id, cancellationToken);

            if (entity is null) throw new EntityNotFoundException($"Workout plan with {model.Id} id does not exist");

            CheckRestrictionsAccess(entity, model.Id, model.UserId ?? entity.UserId);

            _mapper.Map(model, entity);
            await _unitOfWork.WorkoutPlanRepository.Value.UpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteWorkoutAsync(Guid id, string userId, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.WorkoutPlanRepository.Value.GetByIdAsync(id, cancellationToken);

            if (entity is null) throw new EntityNotFoundException($"Workout plan with {id} id was not found");

            CheckRestrictionsAccess(entity, id, userId);

            await _unitOfWork.WorkoutPlanRepository.Value.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}