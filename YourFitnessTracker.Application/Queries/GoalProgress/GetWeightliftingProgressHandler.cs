using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos.GoalProgress;
using MediatR;
using Microsoft.Extensions.Logging;

namespace YourFitnessTracker.Application.Queries.GoalProgress
{
    public record GetWeightliftingProgress(Guid Id) : IRequest<IEnumerable<WeightliftingProgressDto>>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetWeightliftingProgressHandler : IRequestHandler<GetWeightliftingProgress, IEnumerable<WeightliftingProgressDto>>
    {
        private readonly ILogger<GetWeightliftingProgressHandler> _logger;
        private readonly IWeightliftingProgressService _weightliftingProgressService;

        public GetWeightliftingProgressHandler(ILogger<GetWeightliftingProgressHandler> logger, IWeightliftingProgressService weightliftingProgressService)
        {
            _logger = logger;
            _weightliftingProgressService = weightliftingProgressService;
        }

        public Task<IEnumerable<WeightliftingProgressDto>> Handle(GetWeightliftingProgress request, CancellationToken cancellationToken) =>
            _weightliftingProgressService.GetRecordsForGoalAsync(request.Id, request.UserId, cancellationToken);
    }
}
