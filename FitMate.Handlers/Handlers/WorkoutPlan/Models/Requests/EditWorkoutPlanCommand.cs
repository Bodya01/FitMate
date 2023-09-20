using MediatR;

namespace FitMate.Handlers.Handlers.WorkoutPlan.Models.Requests
{
    public class EditWorkoutPlanCommand : IRequest<Unit>
    {
        public DAL.Entities.WorkoutPlan? WorkoutPlan { get; set; }
    }
}
