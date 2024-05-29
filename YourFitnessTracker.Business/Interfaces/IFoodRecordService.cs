using YourFitnessTracker.Infrastructure.Models.FoodRecord;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Business.Interfaces
{
    public interface IFoodRecordService
    {
        Task<IEnumerable<FoodRecordDto>> GetRecordsByDate(DateTime date, string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<FoodRecordDto>> GetRecordsForLastDays(uint previousDays, string userId, CancellationToken cancellationToken = default);
        Task UpdateFoodRecordRangeAsync(IEnumerable<CreateFoodRecordModel> records, string userId, DateTime consumptionDate, CancellationToken cancellationToken = default);
    }
}