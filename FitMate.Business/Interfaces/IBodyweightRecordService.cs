using FitMate.Infrastructure.Models.BodyweightRecord;

namespace FitMate.Business.Interfaces
{
    public interface IBodyweightRecordService
    {
        Task CreateTodayRecordAsync(CreateTodayBodyweightRecordModel model, CancellationToken cancellationToken = default);
        Task CreateBodyweightRecordAsync(CreateBodyweightRecordModel model, CancellationToken cancellationToken = default);
    }
}