using FitMate.Handlers.Handlers.WorkoutPlan.Models.WorkoutPlan.Responses;
using MediatR;

namespace FitMate.Handlers.Handlers.WorkoutPlan.Models.WorkoutPlan.Requests
{
    public class GetWorkoutPlanByIdQuery : IRequest<GetWorkoutPlanByIdResponse>
    {
        public long Id { get; set; }
    }
}
