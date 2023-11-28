using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FitMate.Business.Services
{
    public sealed class UserService : IUserService
    {
        private readonly UserManager<FitnessUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<FitnessUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<FitnessUser?> GetUserAsync(CancellationToken cancellationToken = default) =>
            await Task.Run(() => _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User), cancellationToken);

        public async Task<string> GetUserIdAsync(CancellationToken cancellationToken = default) =>
            (await Task.Run(() => _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User), cancellationToken))!.Id ?? Guid.Empty.ToString();
    }
}