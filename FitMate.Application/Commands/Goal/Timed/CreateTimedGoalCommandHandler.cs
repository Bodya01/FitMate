using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Models.Goal.Timed;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.Goal.Timed
{
    public record CreateTimedGoalCommand(string Activity, int Hours, int Minutes, int Seconds, float Quantity, string QuantityUnit) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class CreateTimedGoalCommandHandler : IRequestHandler<CreateTimedGoalCommand>
    {
        private readonly ITimedGoalService _timedGoalService;
        private readonly ILogger<CreateTimedGoalCommandHandler> _logger;

        public CreateTimedGoalCommandHandler(ITimedGoalService timedGoalService, ILogger<CreateTimedGoalCommandHandler> logger)
        {
            _timedGoalService = timedGoalService;
            _logger = logger;
        }

        public async Task Handle(CreateTimedGoalCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creation of timed goal for user {request.UserId} begins");
            await _timedGoalService.CreateTimedGoalAsync(
                new CreateTimedGoalModel(request.Activity, request.Hours, request.Minutes, request.Seconds, request.Quantity, request.QuantityUnit, request.UserId),
                cancellationToken
            );
            _logger.LogInformation($"Timed goal for user {request.UserId} was successfully created");
        }
    }
}