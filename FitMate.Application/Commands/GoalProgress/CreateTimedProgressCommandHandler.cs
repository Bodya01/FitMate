using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Models.GoalProgress.Timed;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.GoalProgress
{
    public record CreateTimedProgressCommand(DateTime Date, float Quantity, int Hours, int Minutes, int Seconds, Guid GoalId) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class CreateTimedProgressCommandHandler : IRequestHandler<CreateTimedProgressCommand>
    {
        private readonly ILogger<CreateTimedProgressCommandHandler> _logger;
        private readonly ITimedProgressService _timedProgressService;

        public CreateTimedProgressCommandHandler(ILogger<CreateTimedProgressCommandHandler> logger, ITimedProgressService timedProgressService)
        {
            _logger = logger;
            _timedProgressService = timedProgressService;
        }

        public async Task Handle(CreateTimedProgressCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creation of timed progress for goal {request.GoalId} and user {request.UserId}");
            await _timedProgressService.CreateTimedProgressAsync(
                new CreateTimedProgressModel(request.Date, request.Quantity, request.Hours, request.Minutes, request.Seconds, request.GoalId, request.UserId),
                cancellationToken);
            _logger.LogInformation($"Timed progress  for goal {request.GoalId} and user {request.UserId} was successfull");
        }
    }
}