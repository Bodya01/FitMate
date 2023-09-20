using FitMate.Data;
using FitMate.Handlers.Handlers.WorkoutPlan.Models.Requests;
using FitMate.Handlers.Handlers.WorkoutPlan.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Handlers.Handlers.WorkoutPlan.Queries
{
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
