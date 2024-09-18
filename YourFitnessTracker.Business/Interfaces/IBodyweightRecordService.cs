using YourFitnessTracker.Infrastructure.Models.BodyweightRecord;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Business.Interfaces
{
    public interface IBodyweightRecordService
    {
        Task<IEnumerable<BodyweightRecordDto>> GetAllRecordsAsync(string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<BodyweightRecordDto>> GetRecordsByDateAsync(DateTime from, DateTime to, string userId, CancellationToken cancellationToken = default);
        Task<BodyweightRecordDto> GetLastRecordAsync(string userId, CancellationToken cancellationToken = default);
        Task CreateTodayRecordAsync(CreateTodayBodyweightRecordModel model, CancellationToken cancellationToken = default);
        Task CreateBodyweightRecordAsync(CreateBodyweightRecordModel model, CancellationToken cancellationToken = default);
        Task UpdateRangeAsync(IEnumerable<UpdateBodyweightRecordModel> records, string userId, CancellationToken cancellationToken = default);
    }
}