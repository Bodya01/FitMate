using FitMate.Core.Repositories.Interfaces;
using FitMate.Data;
using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Core.Repositories.Implementations
{
    public class GoalRepository : IGoalRepository
    {
        private FitMateContext _context;

        public GoalRepository(FitMateContext context)
        {
            _context = context;
        }

        public async Task DeleteGoalById(string userId, long goalId)
        {
            var existingGoal = await _context.Goals.FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);

            if (existingGoal is null) return;

            _context.Goals.Remove(existingGoal);

            await _context.SaveChangesAsync();
        }

        public async Task<Goal[]> GetAllGoals(string userId) =>
            await _context.Goals.Where(g => g.UserId == userId).ToArrayAsync();

        public async Task<Goal> GetGoalById(string userId, long goalId) =>
            await _context.Goals.FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);

        public async Task<GoalProgress[]> GetGoalProgress(string userId, long goalId, bool ascendingOrder = false)
        {
            var query = _context.GoalProgressRecords.Where(r => r.GoalId == goalId && r.UserId == userId);

            query = ascendingOrder ? query.OrderBy(r => r.Date) : query.OrderByDescending(r => r.Date);

            var result = await query.ToArrayAsync();

            return result;
        }

        public async Task StoreGoal(Goal goal)
        {
            if (goal.Id == 0) _context.Goals.Add(goal);
            else _context.Goals.Update(goal);

            await _context.SaveChangesAsync();
        }

        public async Task StoreGoalProgress(GoalProgress progress)
        {
            _context.GoalProgressRecords.Add(progress);
            await _context.SaveChangesAsync();
        }
    }
}
