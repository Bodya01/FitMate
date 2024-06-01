using MediatR;
using Microsoft.Extensions.Logging;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Models.GoalProgress.Timed;

namespace YourFitnessTracker.Application.Commands.GoalProgress
{
    public record CreateTimedProgress(DateTime Date, float Quantity, Guid GoalId) : IRequest
    {
        public int Hours { get; set; } = default;
        public int Minutes { get; set; } = default;
        public int Seconds { get; set; } = default;
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