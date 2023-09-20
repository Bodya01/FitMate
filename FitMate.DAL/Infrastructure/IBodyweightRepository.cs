using FitMate.Infrastructure.Entities;

namespace FitMate.Data
{
    public interface IBodyweightRepository
    {
        public Task<BodyweightRecord[]> GetBodyweightRecords(FitnessUser user, bool ascendingOrder = false);
        public Task<BodyweightTarget> GetBodyweightTarget(FitnessUser user);
        public Task StoreBodyweightRecord(BodyweightRecord record);
        public Task StoreBodyweightRecords(List<BodyweightRecord> records);
        public Task DeleteExistingRecords(FitnessUser user);
        public Task StoreBodyweightTarget(BodyweightTarget target);
    }
}