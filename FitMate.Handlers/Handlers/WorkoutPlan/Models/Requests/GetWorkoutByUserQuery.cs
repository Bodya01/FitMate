using FitMate.Infrastructure.Entities;
using FitMate.Handlers.Handlers.WorkoutPlan.Models.Responses;
using MediatR;

namespace FitMate.Handlers.Handlers.WorkoutPlan.Models.Requests
{
    public class GetWorkoutByUserQuery : IRequest<GetWorkoutByUserResponse>
    {
        public FitnessUser? User { get; set; }
    }
}
