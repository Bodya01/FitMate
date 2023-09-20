using FitMate.Core.Repositories.Interfaces;
using FitMate.Data;
using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Core.Repositories.Implementations
{
    public class WorkoutPlanRepository : IWorkoutPlanRepository
    {
        private readonly FitMateContext _context;

        public WorkoutPlanRepository(FitMateContext context)
        {
            _context = context;
        }

        public async Task<WorkoutPlan> GetByIdAsync(Guid id) =>
            await _context.WorkoutPlans.FindAsync(id);

        public async Task<List<WorkoutPlan>> GetByUserIdAsync(string userId) =>
            await _context.WorkoutPlans.Where(x => x.UserId == userId)
                .ToListAsync();

        public async Task AddAsync(WorkoutPlan workoutPlan)
        {
            await _context.AddAsync(workoutPlan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WorkoutPlan workoutPlan)
        {
            _context.Update(workoutPlan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var record = await GetByIdAsync(id);

            _context.Remove(record);
            await _context.SaveChangesAsync();
        }
    }
}
