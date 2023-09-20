using FitMate.Core.Repositories.Interfaces;
using FitMate.Data;
using FitMate.Infrastructure.Entities;

namespace FitMate.Core.Repositories.Implementations
{
    public class FoodRepository : IFoodRepository
    {
        private readonly FitMateContext _context;

        public FoodRepository(FitMateContext context)
        {
            _context = context;
        }

        public Task AddAsync(Food food)
        {
            throw new NotImplementedException();
        }

        public Task<List<Food>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Food> GetForUserAndDate(string userId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Food food)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Food food)
        {
            throw new NotImplementedException();
        }
    }
}
