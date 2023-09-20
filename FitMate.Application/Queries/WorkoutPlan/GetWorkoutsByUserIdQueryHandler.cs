using FitMate.Core.UnitOfWork;
using MediatR;

namespace FitMate.Applcation.Queries.WorkoutPlan
{
    public class GetWorkoutByUserIdQuery : IRequest<GetWorkoutByUserIdResponse>
    {
        public string UserId { get; set; }
    }

    public class GetWorkoutByUserIdResponse
    {
        public List<Infrastructure.Entities.WorkoutPlan>? WorkoutPlans { get; set; }
    }

    public class GetWorkoutsByUserIdQueryHandler : IRequestHandler<GetWorkoutByUserIdQuery, GetWorkoutByUserIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWorkoutsByUserIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetWorkoutByUserIdResponse> Handle(GetWorkoutByUserIdQuery request, CancellationToken cancellationToken)
        {
            var plans = await _unitOfWork.WorkoutPlanRepository.Value.GetByUserIdAsync(request.UserId);

            var response = new GetWorkoutByUserIdResponse()
            {
                WorkoutPlans = plans
            };

            return response;
        }
    }
}
