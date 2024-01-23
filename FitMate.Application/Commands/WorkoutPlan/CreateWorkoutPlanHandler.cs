using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Models.WorkoutPlan;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.WorkoutPlan
{
    public record CreateWorkoutPlan(string Name, string SessionsJSON) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class CreateWorkoutPlanHandler : IRequestHandler<CreateWorkoutPlan>
    {
        private readonly ILogger<CreateWorkoutPlanHandler> _logger;
        private readonly IWorkoutPlanService _workoutPlanService;

        public CreateWorkoutPlanHandler(ILogger<CreateWorkoutPlanHandler> logger, IWorkoutPlanService workoutPlanService)
        {
            _logger = logger;
            _workoutPlanService = workoutPlanService;
        }

        public async Task Handle(CreateWorkoutPlan request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creation of workout plan for user {request.UserId} begins");
            await _workoutPlanService.CreateWorkoutPlanAsync(new CreateWorkoutPlanModel(request.Name, request.SessionsJSON, request.UserId), cancellationToken);
            _logger.LogInformation($"Workout plan for user {request.UserId} was successfully created");
        }
    }
}