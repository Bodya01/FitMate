using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using YourFitnessTracker.Applcation.Commands.WorkoutPlan;
using YourFitnessTracker.Applcation.Queries.WorkoutPlan;
using YourFitnessTracker.Application.Commands.WorkoutPlan;
using YourFitnessTracker.Application.Queries.WorkoutPlan;
using YourFitnessTracker.Infrastucture.Dtos;
using YourFitnessTracker.UI.Web.Controllers.Base;

namespace YourFitnessTracker.Controllers
{
    public sealed class WorkoutController : YourFitnessTrackerControllerBase
    {
        public WorkoutController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public IActionResult Index() => RedirectToAction(nameof(Summary));

        [HttpGet]
        public async Task<IActionResult> Summary(CancellationToken cancellationToken)
        {
            var request = new GetWorkoutsForUser(_currentUserId);
            return View(await _mediator.Send(request, cancellationToken));
        }

        [HttpGet]
        public IActionResult Create() => View(WorkoutPlanDto.CreateDefault());

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] GetWorkoutPlan query, CancellationToken cancellationToken)
        {
            query.UserId = _currentUserId;
            return View(await _mediator.Send(query, cancellationToken));
        }

        [HttpGet("Session/{id:guid}/{sessionId:int}")]
        public async Task<IActionResult> Session([FromRoute] GetWorkoutSession query, CancellationToken cancellationToken)
        {
            query.UserId = _currentUserId;
            return View(await _mediator.Send(query, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateWorkoutPlan command, CancellationToken cancellationToken)
        {
            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] EditWorkoutPlan command, CancellationToken cancellationToken)
        {
            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] DeleteWorkoutPlan query, CancellationToken cancellationToken)
        {
            query.UserId = _currentUserId;
            await _mediator.Send(query, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }
    }
}