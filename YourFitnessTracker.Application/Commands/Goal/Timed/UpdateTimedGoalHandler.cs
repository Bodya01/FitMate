using MediatR;
using Microsoft.Extensions.Logging;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Models.Goal.Timed;

namespace YourFitnessTracker.Application.Commands.Goal.Timed
{
    public record UpdateTimedGoal(Guid Id, string Activity, int Hours, int Minutes, int Seconds, float Quantity, string QuantityUnit) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class UpdateTimedGoalHandler : IRequestHandler<UpdateTimedGoal>
    {
        private readonly ILogger<UpdateTimedGoalHandler> _logger;
        private readonly ITimedGoalService _timedGoalService;

        public UpdateTimedGoalHandler(ILogger<UpdateTimedGoalHandler> logger, ITimedGoalService timedGoalService)
        {
            _logger = logger;
            _timedGoalService = timedGoalService;
        }

        public async Task Handle(UpdateTimedGoal request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Update of a timed goal with id {request.Id} begins");
            await _timedGoalService.UpdateTimedGoalAsync(
                new UpdateTimedGoalModel(request.Id, request.Activity, request.Hours, request.Minutes, request.Seconds, request.Quantity, request.QuantityUnit, request.UserId),
                cancellationToken);
            _logger.LogInformation($"Timed goal with id {request.Id} was successfully updated");
        }
    }
}