using AutoMapper;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos.Goals;
using MediatR;

namespace FitMate.Application.Queries.Goal
{
    public record GetGoalQuery(Guid Id) : IRequest<GoalDto>;

    public class GetGoalQueryHandler : IRequestHandler<GetGoalQuery, GoalDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetGoalQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GoalDto> Handle(GetGoalQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GoalRepository.Value.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<GoalDto>(entity);
        }
    }
}