using FitMate.Infrastructure.Entities;

namespace FitMate.Core.Repositories.Interfaces
{
    public interface IGoalRepository
    {
        public Task<Goal[]> GetAllGoals(string userId);
        public Task<Goal> GetGoalById(string userId, long GoalID);
        public Task DeleteGoalById(string userId, long GoalID);
        public Task StoreGoal(Goal Goal);
        public Task StoreGoalProgress(GoalProgress Progress);
        public Task<GoalProgress[]> GetGoalProgress(string userId, long GoalID, bool AscendingOrder = false);
    }
}
