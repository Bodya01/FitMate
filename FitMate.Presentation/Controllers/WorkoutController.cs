using FitMate.Applcation.Commands.WorkoutPlan;
using FitMate.Applcation.Queries.WorkoutPlan;
using FitMate.Application.Commands.WorkoutPlan;
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
    public sealed class WorkoutController : FitMateControllerBase
    {
        public WorkoutController(IMediator mediator, IUnitOfWork unitOfWork, IUserService userService)
            : base(mediator, unitOfWork, userService) { }

        [HttpGet]
        public IActionResult Index() => RedirectToAction(nameof(Summary));

        [HttpGet]
        public async Task<IActionResult> Summary(CancellationToken cancellationToken)
        {
            var request = new GetWorkoutsForUser(await _userService.GetUserIdAsync(cancellationToken));
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
        public async Task<IActionResult> Edit([FromRoute] GetWorkoutPlan query, CancellationToken cancellationToken)
        {
            query.UserId = await _userService.GetUserIdAsync(cancellationToken);
            return View(await _mediator.Send(query, cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> Session([FromRoute] GetWorkoutPlan query, [FromRoute] int sessionId, CancellationToken cancellationToken)
        {
            query.UserId = await _userService.GetUserIdAsync(cancellationToken);
            var plan = await _mediator.Send(query, cancellationToken);

            if (sessionId < 0 || sessionId >= plan.Sessions.Count) return BadRequest();

            var session = plan.Sessions.ToList()[sessionId];
            return View(session);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] EditWorkoutPlan command, CancellationToken cancellationToken)
        {
            command.UserId = await _userService.GetUserIdAsync(cancellationToken);
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] Guid id, CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);
            await _mediator.Send(new DeleteWorkoutPlan(id, currentUserId), cancellationToken);
            return RedirectToAction(nameof(Summary));
        }
    }
}