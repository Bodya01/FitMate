using FitMate.Infrastructure.Entities;

namespace FitMate.Core.Repositories.Interfaces
{
    public interface IFoodRepository
    {
        Task<List<Food>> GetAllAsync();
        Task<Food> GetForUserAndDate(string userId, DateTime date);
        Task AddAsync(Food food);
        Task UpdateAsync(Food food);
        Task RemoveAsync(Food food);
        Task RemoveRangeAsync(string userId);
    }
}
