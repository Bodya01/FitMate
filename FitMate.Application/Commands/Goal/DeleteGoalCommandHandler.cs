using FitMate.Business.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.Goal
{
    public record DeleteGoalCommand(Guid Id, string UserId) : IRequest;

    internal sealed class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand>
    {
        private readonly IGoalService _goalService;
        private readonly ILogger<DeleteGoalCommandHandler> _logger;

        public DeleteGoalCommandHandler(IGoalService goalService, ILogger<DeleteGoalCommandHandler> logger)
        {
            _goalService = goalService;
            _logger = logger;
        }

        public async Task Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"The deletion of goal with {request.Id} id begun");
            await _goalService.DeleteGoalAsync(request.Id, request.UserId, cancellationToken);
            _logger.LogInformation($"Goal with {request.Id} id was successfully deleted");
        }
    }
}