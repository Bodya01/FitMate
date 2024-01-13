using FitMate.Infrastucture.Dtos;

namespace FitMate.Business.Interfaces
{
    public interface IBodyweightRecordService
    {
        Task AddBodyweightRecordsAsync(IEnumerable<BodyweightRecordDto> records, CancellationToken cancellationToken = default);
        Task UpdateBodyweightRecordsAsync(IEnumerable<BodyweightRecordDto> records, CancellationToken cancellationToken = default);
        Task DeleteBodyweightRecordAsync(Guid id, CancellationToken cancellationToken = default);
        Task<WorkoutPlanDto> GetBodyweightRecordAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<WorkoutPlanDto>> GetBodyweightRecordsAsync(string userId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    }
}