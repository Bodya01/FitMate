using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Exceptions;
using YourFitnessTracker.Infrastucture.Dtos;
using MediatR;

namespace YourFitnessTracker.Application.Queries.BodyweightRecord
{
    public record GetBodyweightRecords(string UserId, DateTime From = default, DateTime To = default, bool IgnoreDates = true) : IRequest<List<BodyweightRecordDto>>;

    internal sealed class GetBodyweightRecordsHandler : IRequestHandler<GetBodyweightRecords, List<BodyweightRecordDto>>
    {
        private readonly IBodyweightRecordService _bodyweightRecordService;

        public GetBodyweightRecordsHandler(IBodyweightRecordService bodyweightRecordService)
        {
            _bodyweightRecordService = bodyweightRecordService;
        }

        public async Task<List<BodyweightRecordDto>> Handle(GetBodyweightRecords request, CancellationToken cancellationToken)
        {
            IEnumerable<BodyweightRecordDto> records;

            try
            {
                records = request.IgnoreDates
                ? await _bodyweightRecordService.GetAllRecordsAsync(request.UserId, cancellationToken)
                : await _bodyweightRecordService.GetRecordsByDateAsync(request.From, request.To, request.UserId, cancellationToken);
            }
            catch (EntityNotFoundException)
            {
                records = Enumerable.Empty<BodyweightRecordDto>();
            }

            return records.ToList();
        }
    }
}