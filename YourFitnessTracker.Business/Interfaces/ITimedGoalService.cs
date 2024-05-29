using YourFitnessTracker.Infrastructure.Models.Goal.Timed;
using YourFitnessTracker.Infrastucture.Dtos.Goals;

namespace YourFitnessTracker.Business.Interfaces
{
    public interface ITimedGoalService
    {
        Task<TimedGoalDto> GetTimedGoal(Guid id, string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<TimedGoalDto>> GetTimedGoalsForUser(string userId, CancellationToken cancellationToken = default);
        Task CreateTimedGoalAsync(CreateTimedGoalModel model, CancellationToken cancellationToken = default);
        Task UpdateTimedGoalAsync(UpdateTimedGoalModel model, CancellationToken cancellationToken = default);
    }
}