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

        public async Task DeleteAsync(string userId, Guid goalId)
        {
            var existingGoal = await _context.Goals.FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);

            if (existingGoal is null) return;

            _context.Goals.Remove(existingGoal);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Goal>> GetAllForUserAsync(string userId) =>
            await _context.Goals.Where(g => g.UserId == userId).ToListAsync();

        public async Task<Goal> GetByIdAsync(string userId, Guid goalId) =>
            await _context.Goals.FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);

        public async Task<GoalProgress[]> GetGoalProgressAsync(string userId, Guid goalId, bool ascendingOrder = false)
        {
            var query = _context.GoalProgressRecords.Where(r => r.GoalId == goalId && r.UserId == userId);

            query = ascendingOrder ? query.OrderBy(r => r.Date) : query.OrderByDescending(r => r.Date);

            var result = await query.ToArrayAsync();

            return result;
        }

        public async Task AddAsync(Goal goal)
        {
            await _context.Goals.AddAsync(goal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Goal goal)
        {
            _context.Goals.Update(goal);
            await _context.SaveChangesAsync();
        }
    }
}
