using FitMate.Infrastructure.Entities;

namespace FitMate.Core.Repositories.Interfaces
{
    public interface IBodyweightRepository
    {
        public Task<BodyweightRecord[]> GetBodyweightRecords(string userId, bool ascendingOrder = false);
        public Task<BodyweightTarget> GetBodyweightTarget(string userId);
        public Task StoreBodyweightRecord(BodyweightRecord record);
        public Task StoreBodyweightRecords(List<BodyweightRecord> records);
        public Task DeleteExistingRecords(string userId);
        public Task StoreBodyweightTarget(BodyweightTarget target);
    }
}