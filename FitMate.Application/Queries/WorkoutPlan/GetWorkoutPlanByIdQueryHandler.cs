using FitMate.Core.UnitOfWork;
using FitMate.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Applcation.Queries.WorkoutPlan
{
    public class GetWorkoutPlanByIdQuery : IRequest<GetWorkoutPlanByIdResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetWorkoutPlanByIdResponse
    {
        public Infrastructure.Entities.WorkoutPlan? WorkoutPlan { get; set; }
    }

    public class GetWorkoutPlanByIdQueryHandler : IRequestHandler<GetWorkoutPlanByIdQuery, GetWorkoutPlanByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWorkoutPlanByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetWorkoutPlanByIdResponse> Handle(GetWorkoutPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new GetWorkoutPlanByIdResponse
            {
                WorkoutPlan = await _unitOfWork.WorkoutPlanRepository.Value.GetByIdAsync(request.Id),
            };

            return await Task.FromResult(result);
        }
    }
}
