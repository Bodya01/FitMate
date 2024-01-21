using FitMate.Infrastructure.Models.Goal.Timed;
using FitMate.Infrastucture.Dtos.Goals;

namespace FitMate.Business.Interfaces
{
    public interface ITimedGoalService
    {
        Task<TimedGoalDto> GetTimedGoal(Guid id, string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<TimedGoalDto>> GetTimedGoalsForUser(string userId, CancellationToken cancellationToken = default);
        Task CreateTimedGoalAsync(CreateTimedGoalModel model, CancellationToken cancellationToken = default);
        Task UpdateTimedGoalAsync(UpdateTimedGoalModel model, CancellationToken cancellationToken = default);
    }
}