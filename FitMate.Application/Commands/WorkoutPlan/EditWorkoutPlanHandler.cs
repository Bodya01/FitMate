using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Models.WorkoutPlan;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Applcation.Commands.WorkoutPlan
{
    public record EditWorkoutPlan(Guid Id, string Name, string SessionsJSON) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class EditWorkoutPlanHandler : IRequestHandler<EditWorkoutPlan>
    {
        private readonly ILogger<EditWorkoutPlanHandler> _logger;
        private readonly IWorkoutPlanService _workoutPlanService;

        public EditWorkoutPlanHandler(ILogger<EditWorkoutPlanHandler> logger, IWorkoutPlanService workoutPlanService)
        {
            _logger = logger;
            _workoutPlanService = workoutPlanService;
        }

        public async Task Handle(EditWorkoutPlan request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Update of workout plan with id {request.Id} begins");
            await _workoutPlanService.UpdateWorkoutPlanAsync(
                new UpdateWorkoutPlanModel(request.Id, request.Name, request.SessionsJSON, request.UserId),
                cancellationToken);
            _logger.LogInformation($"Workout plan with id {request.Id} was successfully updated");
        }
    }
}