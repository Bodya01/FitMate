using FitMate.Infrastructure.Entities;

namespace FitMate.Core.Repositories.Interfaces
{
    public interface IBodyweightRecordRepository : IRepositoryBase<BodyweightRecord>
    {
        Task CreateRangeAsync(IEnumerable<BodyweightRecord> bodyweightRecords, CancellationToken cancellationToken = default);
        Task DeleteRangeAsync(IEnumerable<BodyweightRecord> bodyweightRecords, CancellationToken cancellationToken = default);
    }
}