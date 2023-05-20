using FitMate.DAL.Entities;
using System.Threading.Tasks;

namespace FitMate.Data
{
    public interface IGoalRepository
    {
        public Task<Goal[]> GetAllGoals(FitnessUser User);
        public Task<Goal> GetGoalByID(FitnessUser User, long GoalID);
        public Task DeleteGoalByID(FitnessUser User, long GoalID);
        public Task StoreGoal(Goal Goal);
        public Task StoreGoalProgress(GoalProgress Progress);
        public Task<GoalProgress[]> GetGoalProgress(FitnessUser User, long GoalID, bool AscendingOrder = false);
    }
}
