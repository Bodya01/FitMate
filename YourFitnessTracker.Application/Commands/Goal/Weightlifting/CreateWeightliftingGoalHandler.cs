using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Models.Goal.Weightlifting;
using MediatR;
using Microsoft.Extensions.Logging;

namespace YourFitnessTracker.Application.Commands.Goal.Weightlifting
{
    public record CreateWeightliftingGoal(string Activity, float Weight, int Reps) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class CreateWeightliftingGoalHandler : IRequestHandler<CreateWeightliftingGoal>
    {
        private readonly IWeightliftingGoalService _weightliftingGoalService;
        private readonly ILogger<CreateWeightliftingGoalHandler> _logger;

        public CreateWeightliftingGoalHandler(IWeightliftingGoalService weightliftingGoalService, ILogger<CreateWeightliftingGoalHandler> logger)
        {
            _weightliftingGoalService = weightliftingGoalService;
            _logger = logger;
        }

        public async Task Handle(CreateWeightliftingGoal request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creation of weightlifting goal for user {request.UserId} begins");
            await _weightliftingGoalService.CreateWeightliftingGoalAsync(
                new CreateWeightliftingGoalModel(request.Activity, request.Weight, request.Reps, request.UserId),
                cancellationToken
            );
            _logger.LogInformation($"Weightlifting goal for user {request.UserId} was successfully created");
        }
    }
}