using YourFitnessTracker.Infrastructure.Models.WorkoutPlan;
using YourFitnessTracker.Infrastucture.Dtos;

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