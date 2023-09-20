using FitMate.Infrastructure.Entities;

namespace FitMate.Core.Repositories.Interfaces
{
    public interface IBodyweightRecordRepository
    {
        public Task<BodyweightRecord[]> GetAllForUserAsync(string userId, bool ascendingOrder = false);
        public Task AddAsync(BodyweightRecord record);
        public Task AddRangeAsync(List<BodyweightRecord> records);
        public Task DeleteAllForUser(string userId);
    }
}