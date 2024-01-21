using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Models.Goal.Weightlifting;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.Goal.Weightlifting
{
    public record UpdateWeightliftingGoalCommand(Guid Id, string Activity, float Weight, int Reps) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class UpdateWeightliftingGoalCommandHandler : IRequestHandler<UpdateWeightliftingGoalCommand>
    {
        private readonly ILogger<UpdateWeightliftingGoalCommandHandler> _logger;
        private readonly IWeightliftingGoalService _weightliftingGoalService;

        public UpdateWeightliftingGoalCommandHandler(ILogger<UpdateWeightliftingGoalCommandHandler> logger, IWeightliftingGoalService weightliftingGoalService)
        {
            _logger = logger;
            _weightliftingGoalService = weightliftingGoalService;
        }

        public async Task Handle(UpdateWeightliftingGoalCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Update of a weightlifting goal with id {request.Id} begins");
            await _weightliftingGoalService.UpdateWeightliftingGoalAsync(
                new UpdateWeightliftingGoalModel(request.Id, request.Activity, request.Weight, request.Reps, request.UserId),
                cancellationToken);
            _logger.LogInformation($"Weightlifting goal with id {request.Id} was successfully updated");
        }
    }
}