using YourFitnessTracker.Infrastructure.Models.GoalProgress.Timed;
using YourFitnessTracker.Infrastucture.Dtos.GoalProgress;

namespace YourFitnessTracker.Business.Interfaces
{
    public interface ITimedProgressService
    {
        Task<IEnumerable<TimedProgressDto>> GetRecordsForGoalAsync(Guid id, string userId, CancellationToken cancellationToken = default);
        Task CreateTimedProgressAsync(CreateTimedProgressModel model, CancellationToken cancellationToken = default);
    }
}