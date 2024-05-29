using MediatR;
using Microsoft.Extensions.Logging;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Models.BodyweightTarget;

namespace YourFitnessTracker.Application.Commands.BodyweightTarget
{
    public record EditBodyweightTarget(float Weight, DateTime Date) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class EditBodyweightTargetHandler : IRequestHandler<EditBodyweightTarget>
    {
        private readonly ILogger<EditBodyweightTargetHandler> _logger;
        private readonly IBodyweightTargetService _bodyweightTargetService;

        public EditBodyweightTargetHandler(ILogger<EditBodyweightTargetHandler> logger, IBodyweightTargetService bodyweightTargetService)
        {
            _logger = logger;
            _bodyweightTargetService = bodyweightTargetService;
        }

        public async Task Handle(EditBodyweightTarget request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Setting a bodyweight target for user {request.UserId} begun");
            await _bodyweightTargetService.UpdateTargetAsync(new UpdateBodyweightTargetModel(request.Weight, request.Date, request.UserId), cancellationToken);
            _logger.LogInformation($"Bodyweight target for user {request.UserId} was successfully set");
        }
    }
}