using FitMate.Infrastructure.Entities;

namespace FitMate.Core.Repositories.Interfaces
{
    public interface IBodyweightTargetRepository
    {
        public Task<BodyweightTarget> GetForUserAsync(string userId);
        public Task AddAsync(BodyweightTarget target);
        public Task UpdateAsync(BodyweightTarget target);
    }
}