using AutoMapper;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Application.Queries.BodyweightRecord
{
    public record GetBodyweightRecordsQuery(string UserId, DateTime From = default, DateTime To = default, bool IgnoreDates = true) : IRequest<List<BodyweightRecordDto>>;

    public class GetBodyweightRecordsQueryHandler : IRequestHandler<GetBodyweightRecordsQuery, List<BodyweightRecordDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBodyweightRecordsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BodyweightRecordDto>> Handle(GetBodyweightRecordsQuery request, CancellationToken cancellationToken)
        {
            var recordsQuery = _unitOfWork.BodyweightRecordRepository.Value
                .Get(e => e.UserId == request.UserId, s => s);

            if (!request.IgnoreDates)
            {
                recordsQuery.Where(x => x.Date >= request.From && x.Date <= request.To);
            }

            var records = await recordsQuery.ToListAsync(cancellationToken);

            return _mapper.Map<List<BodyweightRecordDto>>(records);
        }
    }
}