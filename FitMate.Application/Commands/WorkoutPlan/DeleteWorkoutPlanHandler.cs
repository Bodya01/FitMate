using FitMate.Business.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.WorkoutPlan
{
    public record DeleteWorkoutPlan(Guid Id) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class DeleteWorkoutPlanHandler : IRequestHandler<DeleteWorkoutPlan>
    {
        private readonly ILogger<DeleteWorkoutPlanHandler> _logger;
        private readonly IWorkoutPlanService _workoutPlanService;

        public DeleteWorkoutPlanHandler(ILogger<DeleteWorkoutPlanHandler> logger, IWorkoutPlanService workoutPlanService)
        {
            _logger = logger;
            _workoutPlanService = workoutPlanService;
        }

        public async Task Handle(DeleteWorkoutPlan request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deletion of workout plan with id {request.Id} begins");
            await _workoutPlanService.DeleteWorkoutAsync(request.Id, request.UserId, cancellationToken);
            _logger.LogInformation($"Deletion of workout plan with id {request.Id} begins");
        }
    }
}
