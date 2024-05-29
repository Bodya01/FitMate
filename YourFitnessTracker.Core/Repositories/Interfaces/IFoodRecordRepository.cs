using YourFitnessTracker.Infrastructure.Entities;

namespace YourFitnessTracker.Core.Repositories.Interfaces
{
    public interface IFoodRecordRepository : IRepositoryBase<FoodRecord>
    {
        Task DeleteRangeAsync(IEnumerable<FoodRecord> records, CancellationToken cancellationToken = default);
        Task CreateRangeAsync(IEnumerable<FoodRecord> entities, CancellationToken cancellationToken = default);
    }
}