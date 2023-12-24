using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Business.Services.Base;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Models.WorkoutPlan;
using FitMate.Infrastucture.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Business.Services
{
    internal sealed class WorkoutPlanService : ServiceBase, IWorkoutPlanService
    {
        public WorkoutPlanService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<WorkoutPlanDto> GetWorkoutAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.WorkoutPlanRepository.Value.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<WorkoutPlanDto>(entity);
        }

        public async Task CreateWorkoutPlanAsync(CreateWorkoutPlanModel workoutPlanModel, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<WorkoutPlan>(workoutPlanModel);

            await _unitOfWork.WorkoutPlanRepository.Value.CreateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateWorkoutPlanAsync(UpdateWorkoutPlanModel workoutPlanModel, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<WorkoutPlan>(workoutPlanModel);

            await _unitOfWork.WorkoutPlanRepository.Value.UpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteWorkoutAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.WorkoutPlanRepository.Value.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<WorkoutPlanDto>> GetWorkoutsAsync(string userId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.WorkoutPlanRepository.Value.Get(
                w => w.UserId == userId,
                s => s)
                .OrderByDescending(w => w.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<WorkoutPlanDto>>(entities).ToList();
        }
    }
}