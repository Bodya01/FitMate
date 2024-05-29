using YourFitnessTracker.Infrastructure.Entities;

namespace YourFitnessTracker.Core.Repositories.Interfaces
{
    public interface IBodyweightRecordRepository : IRepositoryBase<BodyweightRecord>
    {
        Task CreateRangeAsync(IEnumerable<BodyweightRecord> bodyweightRecords, CancellationToken cancellationToken = default);
        Task DeleteRangeAsync(IEnumerable<BodyweightRecord> bodyweightRecords, CancellationToken cancellationToken = default);
    }
}