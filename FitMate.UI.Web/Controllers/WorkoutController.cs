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

        public async Task<IActionResult> Summary()
        {
            var request = new GetWorkoutByUserIdQuery { UserId = await GetUserIdAsync() };
            var response = await _mediator.Send(request);

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
        public async Task<IActionResult> Edit(GetWorkoutPlanByIdQuery query)
        {
            var result = await _mediator.Send(query);

            return View(result.WorkoutPlan);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WorkoutPlan workoutPlan)
        {
            workoutPlan.User = await GetUserAsync();

            var command = new EditWorkoutPlanCommand { WorkoutPlan = workoutPlan };
            await _mediator.Send(command);

            return RedirectToAction("Summary");
        }

        [HttpGet]
        public async Task<IActionResult> Session(Guid workoutPlanId, int sessionId)
        {
            var request = new GetWorkoutPlanByIdQuery { Id = workoutPlanId };
            var plan = await _mediator.Send(request);

            if (plan is null || sessionId < 0 || sessionId >= plan.WorkoutPlan.Sessions.Count) return BadRequest();

            var session = plan.WorkoutPlan.Sessions.ToList()[sessionId];
            return View(session);
        }
    }
}