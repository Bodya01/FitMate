using YourFitnessTracker.Business.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace YourFitnessTracker.Application.Commands.User
{
    public record UpdateUserHeight(string UserId, int Height) : IRequest;

    internal sealed class UpdateUserHeightHandler : IRequestHandler<UpdateUserHeight>
    {
        private readonly ILogger<UpdateUserHeightHandler> _logger;
        private readonly IUserService _userService;

        public UpdateUserHeightHandler(ILogger<UpdateUserHeightHandler> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task Handle(UpdateUserHeight request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Update of height for user {request.UserId} begins");
            await _userService.UpdateUserHeight(request.UserId, request.Height, cancellationToken);
            _logger.LogInformation($"Height for user {request.UserId} was updated successfully");
        }
    }
}