using FitMate.Infrastructure.Models.FoodRecord;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Business.Interfaces
{
    public interface IFoodRecordService
    {
        Task<IEnumerable<FoodRecordDto>> GetRecordsByDate(DateTime date, string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<FoodRecordDto>> GetRecordsForLastDays(uint previousDays, string userId, CancellationToken cancellationToken = default);
        Task UpdateFoodRecordRangeAsync(IEnumerable<CreateFoodRecordModel> records, string userId, DateTime consumptionDate, CancellationToken cancellationToken = default);
    }
}