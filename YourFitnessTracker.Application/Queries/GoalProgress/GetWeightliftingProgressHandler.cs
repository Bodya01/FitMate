using MediatR;
using Microsoft.Extensions.Logging;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos.GoalProgress;

namespace YourFitnessTracker.Application.Queries.GoalProgress
{
    public record GetWeightliftingProgress(Guid Id) : IRequest<IEnumerable<WeightliftingProgressDto>>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetWeightliftingProgressHandler : FitMateRequestHandler<GetWeightliftingProgress, IEnumerable<WeightliftingProgressDto>>
    {
        private readonly ILogger<GetWeightliftingProgressHandler> _logger;
        private readonly IWeightliftingProgressService _weightliftingProgressService;

        public GetWeightliftingProgressHandler(ILogger<GetWeightliftingProgressHandler> logger, IWeightliftingProgressService weightliftingProgressService)
        {
            _logger = logger;
            _weightliftingProgressService = weightliftingProgressService;
        }

        public override async Task<IEnumerable<WeightliftingProgressDto>> Handle(GetWeightliftingProgress request, CancellationToken cancellationToken) =>
            await _weightliftingProgressService.GetRecordsForGoalAsync(request.Id, request.UserId, cancellationToken);
    }
}
