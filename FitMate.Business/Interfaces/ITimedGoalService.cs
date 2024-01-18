using FitMate.Infrastucture.Dtos.Goals;

namespace FitMate.Business.Interfaces
{
    public interface ITimedGoalService
    {
        Task<IEnumerable<TimedGoalDto>> GetTimedGoalsForUser(string userId, CancellationToken cancellationToken = default);
    }
}