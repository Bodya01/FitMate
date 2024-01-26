using FitMate.Infrastructure.Models.BodyweightRecord;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Business.Interfaces
{
    public interface IBodyweightRecordService
    {
        Task<IEnumerable<BodyweightRecordDto>> GetAllRecordsAsync(string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<BodyweightRecordDto>> GetRecordsByDateAsync(DateTime from, DateTime to, string userId, CancellationToken cancellationToken = default);
        Task CreateTodayRecordAsync(CreateTodayBodyweightRecordModel model, CancellationToken cancellationToken = default);
        Task CreateBodyweightRecordAsync(CreateBodyweightRecordModel model, CancellationToken cancellationToken = default);
    }
}