using FitMate.Applcation.Commands.WorkoutPlan;
using FitMate.Applcation.Queries.WorkoutPlan;
using FitMate.Business.Interfaces;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitMate.Controllers
{
    internal sealed class WorkoutController : FitMateControllerBase
    {
        public WorkoutController(IMediator mediator, IUnitOfWork unitOfWork, IUserService userService)
            : base(mediator, unitOfWork, userService) { }

        [HttpGet]
        public IActionResult Index() => RedirectToAction(nameof(Summary));

        [HttpGet]
        public async Task<IActionResult> Summary(CancellationToken cancellationToken)
        {
            var request = new GetWorkoutForUserQuery(await _userService.GetUserIdAsync(cancellationToken));
            var response = await _mediator.Send(request, cancellationToken);

            return View(response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var newPlan = new WorkoutPlanDto(Guid.Empty, "Workout Plan", null);
            return View("/Views/Workout/Edit.cshtml", newPlan);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(GetWorkoutPlanQuery query, CancellationToken cancellationToken) =>
            View(await _mediator.Send(query, cancellationToken));

        [HttpGet]
        public async Task<IActionResult> Session(Guid workoutPlanId, int sessionId, CancellationToken cancellationToken)
        {
            var request = new GetWorkoutPlanQuery(workoutPlanId);
            var plan = await _mediator.Send(request, cancellationToken);

            if (plan is null || sessionId < 0 || sessionId >= plan.Sessions.Count) return BadRequest();

            var session = plan.Sessions.ToList()[sessionId];
            return View(session);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WorkoutPlanDto workoutPlanDto, CancellationToken cancellationToken)
        {
            workoutPlanDto.UserId = await _userService.GetUserIdAsync(cancellationToken);

            var command = new EditWorkoutPlanCommand(workoutPlanDto);
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }
    }
}