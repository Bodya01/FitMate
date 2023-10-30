using FitMate.Core.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Application.Queries.Goal
{
    public record GetGoalsQuery(string UserId) : IRequest<List<Infrastructure.Entities.Goal>>;
    public class GetGoalsQueryHandler : IRequestHandler<GetGoalsQuery, List<Infrastructure.Entities.Goal>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetGoalsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Infrastructure.Entities.Goal>> Handle(GetGoalsQuery request, CancellationToken cancellationToken)
        {
            var goals = await _unitOfWork.GoalRepository.Value
                .Get(e => e.UserId == request.UserId, s => s)
                .ToListAsync(cancellationToken);

            return goals;
        }
    }
}
