using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Models.GoalProgress.Weightlifting;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.GoalProgress
{
    public record CreateWeightliftingProgressCommand(DateTime Date, float Weight, int Reps, Guid GoalId) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class CreateWeightliftingProgressCommandHandler : IRequestHandler<CreateWeightliftingProgressCommand>
    {
        private readonly ILogger<CreateWeightliftingProgressCommandHandler> _logger;
        private readonly IWeightliftingProgressService _weightliftingProgressService;

        public CreateWeightliftingProgressCommandHandler(ILogger<CreateWeightliftingProgressCommandHandler> logger,
            IWeightliftingProgressService weightliftingProgressService)
        {
            _logger = logger;
            _weightliftingProgressService = weightliftingProgressService;
        }

        public async Task Handle(CreateWeightliftingProgressCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creation of weightlifting progress for goal {request.GoalId} and user {request.UserId}");
            await _weightliftingProgressService.CreateWeightliftingProgressAsync(
                new CreateWeightliftingProgressModel(request.Date, request.Weight, request.Reps, request.GoalId, request.UserId),
                cancellationToken);
            _logger.LogInformation($"Weightlifting progress  for goal {request.GoalId} and user {request.UserId} was successfull");
        }
    }
}