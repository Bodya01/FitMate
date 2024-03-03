using FitMate.Core.Context;
using FitMate.Core.Repositories.Interfaces;
using FitMate.Infrastructure.Entities;

namespace FitMate.Core.Repositories.Implementations
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly FitMateContext _context;

        public UserRepository(FitMateContext context)
        {
            _context = context;
        }

        public async Task UpdateAsync(FitnessUser user, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Update(user), cancellationToken);
    }
}