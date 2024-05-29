using YourFitnessTracker.Core.Context;
using YourFitnessTracker.Core.Repositories.Interfaces;
using YourFitnessTracker.Infrastructure.Entities;

namespace YourFitnessTracker.Core.Repositories.Implementations
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly YourFitnessTrackerContext _context;

        public UserRepository(YourFitnessTrackerContext context)
        {
            _context = context;
        }

        public async Task UpdateAsync(FitnessUser user, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Update(user), cancellationToken);
    }
}