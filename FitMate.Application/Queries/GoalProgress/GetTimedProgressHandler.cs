using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos.GoalProgress;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Queries.GoalProgress
{
    public record GetTimedProgress(Guid Id) : IRequest<IEnumerable<TimedProgressDto>>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetTimedProgressHandler : IRequestHandler<GetTimedProgress, IEnumerable<TimedProgressDto>>
    {
        private readonly ILogger<GetTimedProgressHandler> _logger;
        private readonly ITimedProgressService _timedProgressService;

        public GetTimedProgressHandler(ILogger<GetTimedProgressHandler> logger, ITimedProgressService timedProgressService)
        {
            _logger = logger;
            _timedProgressService = timedProgressService;
        }

        public async Task<IEnumerable<TimedProgressDto>> Handle(GetTimedProgress request, CancellationToken cancellationToken) =>
            await _timedProgressService.GetRecordsForGoalAsync(request.Id, request.UserId, cancellationToken);
    }
}