using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos;
using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Application.Queries.Bodyweight
{
    public record GetBodyweightSummary(string UserId) : IRequest<(BodyweightTargetDto? target, List<BodyweightRecordDto> records)>;

    internal sealed class GetBodyweightSummaryHandler : FitMateRequestHandler<GetBodyweightSummary, (BodyweightTargetDto? target, List<BodyweightRecordDto> records)>
    {
        private readonly IBodyweightTargetService _bodyweightTargetService;
        private readonly IBodyweightRecordService _bodyweightRecordService;

        public GetBodyweightSummaryHandler(IBodyweightTargetService bodyweightTargetService, IBodyweightRecordService bodyweightRecordService)
        {
            _bodyweightRecordService = bodyweightRecordService;
            _bodyweightTargetService = bodyweightTargetService;
        }

        public override async Task<(BodyweightTargetDto? target, List<BodyweightRecordDto> records)> Handle(GetBodyweightSummary request, CancellationToken cancellationToken)
        {
            var records = await TryGetCollectionAsync(_bodyweightRecordService.GetAllRecordsAsync(request.UserId, cancellationToken));
            var target = await TryGetModelAsync(_bodyweightTargetService.GetCurrentTargetAsync(request.UserId, cancellationToken));

            return (target, records.ToList());
        }
    }
}