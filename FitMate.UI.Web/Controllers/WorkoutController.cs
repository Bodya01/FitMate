using FitMate.Infrastructure.Entities;
using FitMate.Data;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using FitMate.Applcation.Queries.WorkoutPlan;
using FitMate.Applcation.Commands.WorkoutPlan;

namespace FitMate.Controllers
{
    [Authorize]
    [Controller]
    public class WorkoutController : FitMateControllerBase
    {
        public WorkoutController(FitMateContext context, UserManager<FitnessUser> userManager, IMediator mediator) : base(context, userManager, mediator) { }

        public async Task<IActionResult> Summary()
        {
            var currentUser = await GetUserAsync();

            var request = new GetWorkoutByUserQuery { User = currentUser };
            var response = await _mediator.Send(request);

            return View(response.WorkoutPlans);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var newPlan = new WorkoutPlan()
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
            var currentUser = await GetUserAsync();
            workoutPlan.User = currentUser;

            var command = new EditWorkoutPlanCommand { WorkoutPlan = workoutPlan };
            await _mediator.Send(command);

            return RedirectToAction("Summary");
        }

        [HttpGet]
        public async Task<IActionResult> Session(long workoutPlanId, int sessionId)
        {
            var request = new GetWorkoutPlanByIdQuery { Id = workoutPlanId };
            var plan = await _mediator.Send(request);

            if (plan is null || sessionId < 0 || sessionId >= plan.WorkoutPlan.Sessions.Count) return BadRequest();

            var session = plan.WorkoutPlan.Sessions.ToList()[sessionId];
            return View(session);
        }
    }
}