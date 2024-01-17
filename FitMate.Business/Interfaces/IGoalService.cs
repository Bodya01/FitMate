using FitMate.Infrastucture.Dtos.Goals;

namespace FitMate.Business.Interfaces
{
    public interface IGoalService
    {
        Task<GoalDto> GetGoalAsync(Guid id, string userId, CancellationToken cancellationToken = default);
        Task DeleteGoalAsync(Guid id, string userId, CancellationToken cancellationToken = default);
    }
}