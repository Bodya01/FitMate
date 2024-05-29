using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Models.Goal.Weightlifting;
using MediatR;
using Microsoft.Extensions.Logging;

namespace YourFitnessTracker.Application.Commands.Goal.Weightlifting
{
    public record UpdateWeightliftingGoal(Guid Id, string Activity, float Weight, int Reps) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class UpdateWeightliftingGoalHandler : IRequestHandler<UpdateWeightliftingGoal>
    {
        private readonly ILogger<UpdateWeightliftingGoalHandler> _logger;
        private readonly IWeightliftingGoalService _weightliftingGoalService;

        public UpdateWeightliftingGoalHandler(ILogger<UpdateWeightliftingGoalHandler> logger, IWeightliftingGoalService weightliftingGoalService)
        {
            _logger = logger;
            _weightliftingGoalService = weightliftingGoalService;
        }

        public async Task Handle(UpdateWeightliftingGoal request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Update of a weightlifting goal with id {request.Id} begins");
            await _weightliftingGoalService.UpdateWeightliftingGoalAsync(
                new UpdateWeightliftingGoalModel(request.Id, request.Activity, request.Weight, request.Reps, request.UserId),
                cancellationToken);
            _logger.LogInformation($"Weightlifting goal with id {request.Id} was successfully updated");
        }
    }
}