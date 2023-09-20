using FitMate.Infrastructure.Entities;

namespace FitMate.Core.Repositories.Interfaces
{
    public interface IGoalRepository
    {
        public Task<List<Goal>> GetAllForUserAsync(string userId);
        public Task<Goal> GetByIdAsync(string userId, Guid goalId);
        public Task DeleteAsync(string userId, Guid goalId);
        public Task AddAsync(Goal Goal);
        public Task UpdateAsync(Goal Goal);
    }
}