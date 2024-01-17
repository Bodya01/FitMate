using FitMate.Infrastructure.Models.WorkoutPlan;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Business.Interfaces
{
    public interface IWorkoutPlanService
    {
        Task CreateWorkoutPlanAsync(CreateWorkoutPlanModel workoutPlanModel, CancellationToken cancellationToken = default);
        Task UpdateWorkoutPlanAsync(UpdateWorkoutPlanModel workoutPlanModel, CancellationToken cancellationToken = default);
        Task DeleteWorkoutAsync(Guid id, string userId, CancellationToken cancellationToken = default);
        Task<WorkoutPlanDto> GetWorkoutAsync(Guid id, string userId, CancellationToken cancellationToken = default);
        Task<List<WorkoutPlanDto>> GetWorkoutsAsync(string userId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    }
}