using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Models.Goal.Timed;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.Goal.Timed
{
    public record UpdateTimedGoalCommand(Guid Id, string Activity, int Hours, int Minutes, int Seconds, float Quantity, string QuantityUnit) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class UpdateTimedGoalCommandHandler : IRequestHandler<UpdateTimedGoalCommand>
    {
        private readonly ILogger<UpdateTimedGoalCommandHandler> _logger;
        private readonly ITimedGoalService _timedGoalService;

        public UpdateTimedGoalCommandHandler(ILogger<UpdateTimedGoalCommandHandler> logger, ITimedGoalService timedGoalService)
        {
            _logger = logger;
            _timedGoalService = timedGoalService;
        }

        public async Task Handle(UpdateTimedGoalCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Update of a timed goal with id {request.Id} begins");
            await _timedGoalService.UpdateTimedGoalAsync(
                new UpdateTimedGoalModel(request.Id, request.Activity, request.Hours, request.Minutes, request.Seconds, request.Quantity, request.QuantityUnit, request.UserId),
                cancellationToken);
            _logger.LogInformation($"Timed goal with id {request.Id} was successfully updated");
        }
    }
}