using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Queries.BodyweightRecord
{
    public record GetBodyweightRecords(string UserId, DateTime From = default, DateTime To = default, bool IgnoreDates = true) : IRequest<List<BodyweightRecordDto>>;

    internal sealed class GetBodyweightRecordsHandler : IRequestHandler<GetBodyweightRecords, List<BodyweightRecordDto>>
    {
        private readonly ILogger<GetBodyweightRecordsHandler> _logger;
        private readonly IBodyweightRecordService _bodyweightRecordService;

        public GetBodyweightRecordsHandler(ILogger<GetBodyweightRecordsHandler> logger, IBodyweightRecordService bodyweightRecordService)
        {
            _logger = logger;
            _bodyweightRecordService = bodyweightRecordService;
        }

        public async Task<List<BodyweightRecordDto>> Handle(GetBodyweightRecords request, CancellationToken cancellationToken)
        {
            var records = request.IgnoreDates
                ? await _bodyweightRecordService.GetAllRecordsAsync(request.UserId, cancellationToken)
                : await _bodyweightRecordService.GetRecordsByDateAsync(request.From, request.To, request.UserId, cancellationToken);

            return records.ToList();
        }
    }
}