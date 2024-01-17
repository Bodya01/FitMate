using FitMate.Business.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.WorkoutPlan
{
    public record DeleteWorkoutPlanCommand(Guid Id, string userId) : IRequest;

    internal sealed class DeleteWorkoutPlanCommandHandler : IRequestHandler<DeleteWorkoutPlanCommand>
    {
        private readonly ILogger<DeleteWorkoutPlanCommandHandler> _logger;
        private readonly IWorkoutPlanService _workoutPlanService;

        public DeleteWorkoutPlanCommandHandler(ILogger<DeleteWorkoutPlanCommandHandler> logger, IWorkoutPlanService workoutPlanService)
        {
            _logger = logger;
            _workoutPlanService = workoutPlanService;
        }

        public async Task Handle(DeleteWorkoutPlanCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deletion of workout plan with id {request.Id} begins");
            await _workoutPlanService.DeleteWorkoutAsync(request.Id, request.userId, cancellationToken);
            _logger.LogInformation($"Deletion of workout plan with id {request.Id} begins");
        }
    }
}
