using FitMate.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FitMate.Data
{
    public class GoalRepository : IGoalRepository
    {
        private FitMateContext dbContext;

        public GoalRepository(FitMateContext DBContext)
        {
            dbContext = DBContext;
        }

        public async Task DeleteGoalByID(FitnessUser User, long GoalID)
        {
            var existingGoal = await dbContext.Goals.FirstOrDefaultAsync(goal => goal.Id == GoalID && goal.User == User);
            if (existingGoal == null)
                return;

            dbContext.Goals.Remove(existingGoal);

            await dbContext.SaveChangesAsync();
        }

        public async Task<Goal[]> GetAllGoals(FitnessUser User)
        {
            var result = await dbContext.Goals.Where(goal => goal.User == User).ToArrayAsync();
            return result;
        }

        public async Task<Goal> GetGoalByID(FitnessUser User, long GoalID)
        {
            var result = await dbContext.Goals.FirstOrDefaultAsync(goal => goal.Id == GoalID && goal.User == User);
            return result;
        }

        public async Task<GoalProgress[]> GetGoalProgress(FitnessUser User, long GoalID, bool AscendingOrder = false)
        {
            var query = dbContext.GoalProgressRecords
                .Where(record => record.Goal.Id == GoalID && record.User == User);
            if (AscendingOrder == true)
                query = query.OrderBy(record => record.Date);
            else
                query = query.OrderByDescending(record => record.Date);

            var result = await query.ToArrayAsync();

            return result;
        }

        public async Task StoreGoal(Goal Goal)
        {
            if (Goal.Id == 0)
                dbContext.Goals.Add(Goal);
            else
                dbContext.Goals.Update(Goal);

            await dbContext.SaveChangesAsync();
        }

        public async Task StoreGoalProgress(GoalProgress Progress)
        {
            dbContext.GoalProgressRecords.Add(Progress);
            await dbContext.SaveChangesAsync();
        }
    }
}
