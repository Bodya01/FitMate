using FitMate.Infrastructure.Models.FoodRecord;

namespace FitMate.Business.Interfaces
{
    public interface IFoodRecordService
    {
        Task UpdateFoodRecordRangeAsync(IEnumerable<CreateFoodRecordModel> records, string userId, DateTime consumptionDate, CancellationToken cancellationToken = default);
    }
}