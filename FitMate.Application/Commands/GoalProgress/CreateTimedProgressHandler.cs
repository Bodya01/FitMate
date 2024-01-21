using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Models.GoalProgress.Timed;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.GoalProgress
{
    public record CreateTimedProgress(DateTime Date, float Quantity, int Hours, int Minutes, int Seconds, Guid GoalId) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class CreateTimedProgressHandler : IRequestHandler<CreateTimedProgress>
    {
        private readonly ILogger<CreateTimedProgressHandler> _logger;
        private readonly ITimedProgressService _timedProgressService;

        public CreateTimedProgressHandler(ILogger<CreateTimedProgressHandler> logger, ITimedProgressService timedProgressService)
        {
            _logger = logger;
            _timedProgressService = timedProgressService;
        }

        public async Task Handle(CreateTimedProgress request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creation of timed progress for goal {request.GoalId} and user {request.UserId}");
            await _timedProgressService.CreateTimedProgressAsync(
                new CreateTimedProgressModel(request.Date, request.Quantity, request.Hours, request.Minutes, request.Seconds, request.GoalId, request.UserId),
                cancellationToken);
            _logger.LogInformation($"Timed progress  for goal {request.GoalId} and user {request.UserId} was successfull");
        }
    }
}