using FitMate.Infrastructure.Models.GoalProgress.Timed;
using FitMate.Infrastucture.Dtos.GoalProgress;

namespace FitMate.Business.Interfaces
{
    public interface ITimedProgressService
    {
        Task<IEnumerable<TimedProgressDto>> GetRecordsForGoalAsync(Guid id, string userId, CancellationToken cancellationToken = default);
        Task CreateTimedProgressAsync(CreateTimedProgressModel model, CancellationToken cancellationToken = default);
    }
}