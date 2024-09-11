using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Exceptions;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Application.Queries.BodyweightRecord
{
    public record GetBodyweightRecords(string UserId, DateTime From = default, DateTime To = default, bool IgnoreDates = true) : IRequest<List<BodyweightRecordDto>>;

    internal sealed class GetBodyweightRecordsHandler : FitMateRequestHandler<GetBodyweightRecords, List<BodyweightRecordDto>>
    {
        private readonly IBodyweightRecordService _bodyweightRecordService;

        public GetBodyweightRecordsHandler(IBodyweightRecordService bodyweightRecordService)
        {
            _bodyweightRecordService = bodyweightRecordService;
        }

        public override async Task<List<BodyweightRecordDto>> Handle(GetBodyweightRecords request, CancellationToken cancellationToken)
        {
            var records = request.IgnoreDates
                ? await TryGetCollectionAsync(_bodyweightRecordService.GetAllRecordsAsync(request.UserId, cancellationToken))
                : await TryGetCollectionAsync(_bodyweightRecordService.GetRecordsByDateAsync(request.From, request.To, request.UserId, cancellationToken));

            return records.ToList();
        }
    }
}