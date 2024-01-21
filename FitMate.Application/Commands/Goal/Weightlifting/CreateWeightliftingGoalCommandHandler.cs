using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Models.Goal.Weightlifting;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.Goal.Weightlifting
{
    public record CreateWeightliftingGoalCommand(string Activity, float Weight, int Reps) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class CreateWeightliftingGoalCommandHandler : IRequestHandler<CreateWeightliftingGoalCommand>
    {
        private readonly IWeightliftingGoalService _weightliftingGoalService;
        private readonly ILogger<CreateWeightliftingGoalCommandHandler> _logger;

        public CreateWeightliftingGoalCommandHandler(IWeightliftingGoalService weightliftingGoalService, ILogger<CreateWeightliftingGoalCommandHandler> logger)
        {
            _weightliftingGoalService = weightliftingGoalService;
            _logger = logger;
        }

        public async Task Handle(CreateWeightliftingGoalCommand request, CancellationToken cancellationToken)
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