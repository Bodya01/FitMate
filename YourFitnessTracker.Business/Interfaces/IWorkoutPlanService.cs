using YourFitnessTracker.Infrastructure.Dtos;
using YourFitnessTracker.Infrastructure.Models.WorkoutPlan;

namespace YourFitnessTracker.Business.Interfaces
{
    public interface IWorkoutPlanService
    {
        Task CreateWorkoutPlanAsync(CreateWorkoutPlanModel workoutPlanModel, CancellationToken cancellationToken = default);
        Task UpdateWorkoutPlanAsync(UpdateWorkoutPlanModel workoutPlanModel, CancellationToken cancellationToken = default);
        Task DeleteWorkoutAsync(Guid id, string userId, CancellationToken cancellationToken = default);
        Task<WorkoutPlanDto> GetWorkoutAsync(Guid id, string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<WorkoutPlanDto>> GetWorkoutsAsync(string userId, CancellationToken cancellationToken = default);
    }
}