using FitMate.Infrastructure.Entities;
using FitMate.Data;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using FitMate.Applcation.Queries.WorkoutPlan;
using FitMate.Applcation.Commands.WorkoutPlan;
using FitMate.Core.UnitOfWork;
using System;
using System.Threading;

namespace FitMate.Controllers
{
    public class WorkoutController : FitMateControllerBase
    {
        public WorkoutController(FitMateContext context,
            UserManager<FitnessUser> userManager,
            IMediator mediator,
            IUnitOfWork unitOfWork)
            : base(context,
                  userManager,
                  mediator,
                  unitOfWork) { }

        public async Task<IActionResult> Summary(CancellationToken cancellationToken = default)
        {
            var request = new GetWorkoutByUserIdQuery { UserId = await GetUserIdAsync(cancellationToken) };
            var response = await _mediator.Send(request, cancellationToken);

            return View(response.WorkoutPlans);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var newPlan = new WorkoutPlan
            {
                Name = "Workout Plan"
            };

            return View("/Views/Workout/Edit.cshtml", newPlan);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(GetWorkoutPlanByIdQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return View(result.WorkoutPlan);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WorkoutPlan workoutPlan, CancellationToken cancellationToken = default)
        {
            workoutPlan.User = await GetUserAsync(cancellationToken);

            var command = new EditWorkoutPlanCommand { WorkoutPlan = workoutPlan };
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction("Summary");
        }

        [HttpGet]
        public async Task<IActionResult> Session(Guid workoutPlanId, int sessionId, CancellationToken cancellationToken = default)
        {
            var request = new GetWorkoutPlanByIdQuery { Id = workoutPlanId };
            var plan = await _mediator.Send(request, cancellationToken);

            if (plan is null || sessionId < 0 || sessionId >= plan.WorkoutPlan.Sessions.Count) return BadRequest();

            var session = plan.WorkoutPlan.Sessions.ToList()[sessionId];
            return View(session);
        }
    }
}