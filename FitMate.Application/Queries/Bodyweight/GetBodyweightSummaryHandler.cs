using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Exceptions;
using FitMate.Infrastucture.Dtos;
using FitMate.Infrastucture.Dtos.Base;
using MediatR;

namespace FitMate.Application.Queries.Bodyweight
{
    public record GetBodyweightSummary(string UserId) : IRequest<(BodyweightTargetDto target, List<BodyweightRecordDto> records)>;

    internal sealed class GetBodyweightSummaryHandler : IRequestHandler<GetBodyweightSummary, (BodyweightTargetDto target, List<BodyweightRecordDto> records)>
    {
        private readonly IBodyweightTargetService _bodyweightTargetService;
        private readonly IBodyweightRecordService _bodyweightRecordService;

        public GetBodyweightSummaryHandler(IBodyweightTargetService bodyweightTargetService, IBodyweightRecordService bodyweightRecordService)
        {
            _bodyweightRecordService = bodyweightRecordService;
            _bodyweightTargetService = bodyweightTargetService;
        }

        public async Task<(BodyweightTargetDto target, List<BodyweightRecordDto> records)> Handle(GetBodyweightSummary request, CancellationToken cancellationToken)
        {
            var records = await GetRecordsAsync(request.UserId, cancellationToken);
            var target = await GetTargetAsync(request.UserId, cancellationToken);

            return (target, records);
        }

        private async Task<List<BodyweightRecordDto>> GetRecordsAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                return (await _bodyweightRecordService.GetAllRecordsAsync(userId, cancellationToken)).ToList();
            }
            catch (EntityNotFoundException)
            {
                return new List<BodyweightRecordDto>();
            }
        }

        private async Task<BodyweightTargetDto> GetTargetAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                return await _bodyweightTargetService.GetCurrentTargetAsync(userId, cancellationToken);
            }
            catch (EntityNotFoundException)
            {
                return null;
            }
        }
    }
}