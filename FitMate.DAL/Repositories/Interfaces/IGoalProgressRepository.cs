using FitMate.Infrastructure.Entities;

namespace FitMate.Core.Repositories.Interfaces
{
    public interface IGoalProgressRepository
    {
        public Task<List<GoalProgress>> GetForUserAsync(string userId, Guid goalId, bool AscendingOrder = false);
        public Task AddAsync(GoalProgress Progress);
    }
}