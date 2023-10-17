using AutoMapper;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Application.Queries.BodyweightTarget
{
    public record GetCurrentBodyweightTargetQuery(string UserId) : IRequest<BodyweightTargetDto>;

    public class GetCurrentBodyweightTargetQueryHandler : IRequestHandler<GetCurrentBodyweightTargetQuery, BodyweightTargetDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCurrentBodyweightTargetQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BodyweightTargetDto> Handle(GetCurrentBodyweightTargetQuery request, CancellationToken cancellationToken)
        {
            var target = await _unitOfWork.BodyweightTargetRepository.Value
                .Get(e => e.UserId == request.UserId, s => s)
                .OrderByDescending(e => e.TargetDate)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<BodyweightTargetDto>(target);
        }
    }
}