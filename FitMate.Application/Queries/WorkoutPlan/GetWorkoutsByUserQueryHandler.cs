using FitMate.Data;
using FitMate.Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Applcation.Queries.WorkoutPlan
{
    public class GetWorkoutByUserQuery : IRequest<GetWorkoutByUserResponse>
    {
        public FitnessUser? User { get; set; }
    }

    public class GetWorkoutByUserResponse
    {
        public List<Infrastructure.Entities.WorkoutPlan>? WorkoutPlans { get; set; }
    }

    public class GetWorkoutsByUserQueryHandler : IRequestHandler<GetWorkoutByUserQuery, GetWorkoutByUserResponse>
    {
        private readonly FitMateContext _context;

        public GetWorkoutsByUserQueryHandler(FitMateContext context)
        {
            _context = context;
        }

        public async Task<GetWorkoutByUserResponse> Handle(GetWorkoutByUserQuery request, CancellationToken cancellationToken)
        {
            var plans = await _context.WorkoutPlans.Where(plan => plan.User == request.User).ToListAsync();

            var response = new GetWorkoutByUserResponse()
            {
                WorkoutPlans = plans
            };

            return response;
        }
    }
}
