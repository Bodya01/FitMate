using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos.GoalProgress;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Queries.GoalProgress
{
    public record GetWeightliftingProgressQuery(Guid GoalId) : IRequest<IEnumerable<WeightliftingProgressDto>>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetWeightliftingProgressQueryHandler : IRequestHandler<GetWeightliftingProgressQuery, IEnumerable<WeightliftingProgressDto>>
    {
        private readonly ILogger<GetWeightliftingProgressQueryHandler> _logger;
        private readonly IWeightliftingProgressService _weightliftingProgressService;

        public GetWeightliftingProgressQueryHandler(ILogger<GetWeightliftingProgressQueryHandler> logger, IWeightliftingProgressService weightliftingProgressService)
        {
            _logger = logger;
            _weightliftingProgressService = weightliftingProgressService;
        }

        public Task<IEnumerable<WeightliftingProgressDto>> Handle(GetWeightliftingProgressQuery request, CancellationToken cancellationToken) =>
            _weightliftingProgressService.GetRecordsForGoalAsync(request.GoalId, request.UserId, cancellationToken);
    }
}
