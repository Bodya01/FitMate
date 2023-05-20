using FitMate.DAL.Entities;
using FitMate.Data;
using FitMate.Handlers.Handlers.WorkoutPlan.Models.WorkoutPlan.Requests;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FitMate.Controllers
{
    [Authorize]
    [Controller]
    public class WorkoutController : FitMateControllerBase
    {
        public WorkoutController(FitMateContext context, UserManager<FitnessUser> userManager, IMediator mediator) : base(context, userManager, mediator)
        {

        }

        public async Task<IActionResult> Summary()
        {
            var currentUser = await GetUserAsync();
            var plans = await _context.WorkoutPlans.Where(plan => plan.User == currentUser).ToArrayAsync();

            return View(plans);
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

            if (workoutPlan.Id == 0)
            {
                _context.WorkoutPlans.Add(workoutPlan);
            }
            else
            {
                _context.WorkoutPlans.Update(workoutPlan);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Summary");
        }

        [HttpGet]
        public async Task<IActionResult> Session(long workoutPlanId, int sessionId)
        {
            var currentUser = await GetUserAsync();
            var plan = await _context.WorkoutPlans.FirstOrDefaultAsync(plan => plan.Id == workoutPlanId && plan.User == currentUser);

            if (plan == null || sessionId < 0 || sessionId >= plan.Sessions.Length)
                return BadRequest();

            var session = plan.Sessions[sessionId];
            return View(session);
        }
    }
}
