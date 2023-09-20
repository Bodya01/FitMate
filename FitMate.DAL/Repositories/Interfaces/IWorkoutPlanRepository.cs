using FitMate.Infrastructure.Entities;

namespace FitMate.Core.Repositories.Interfaces
{
    public interface IWorkoutPlanRepository
    {
        Task<WorkoutPlan> GetByIdAsync(Guid id);
        Task<List<WorkoutPlan>> GetByUserIdAsync(string userId);
        Task AddAsync(WorkoutPlan workoutPlan);
        Task UpdateAsync(WorkoutPlan workoutPlan);
        Task DeleteAsync(Guid id);
    }
}
