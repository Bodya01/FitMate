using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using YourFitnessTracker.Application.Commands.Goal.Timed;
using YourFitnessTracker.Application.Commands.Goal.Weightlifting;
using YourFitnessTracker.Application.Queries.Goal;
using YourFitnessTracker.Application.Queries.Goal.Timed;
using YourFitnessTracker.Application.Queries.Goal.Weightlifting;
using YourFitnessTracker.Infrastucture.Dtos.Goals;
using YourFitnessTracker.Presentation.ViewModels.Goal;
using YourFitnessTracker.UI.Web.Controllers.Base;

namespace YourFitnessTracker.Controllers
{
    public sealed class GoalController : YourFitnessTrackerControllerBase
    {
        public GoalController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public IActionResult Index() => RedirectToAction(nameof(Summary));

        [HttpGet]
        public async Task<IActionResult> Summary(CancellationToken cancellationToken)
        {
            var (weightlifting, timed) = await _mediator.Send(new GoalSummaryQuery(_currentUserId), cancellationToken);
            return View(new GoalSummaryViewModel(weightlifting, timed));
        }

        [HttpGet]
        public IActionResult Add() => View("Add", WeightliftingGoalDto.CreateDefault());

        [HttpGet]
        public async Task<IActionResult> EditTimed([FromRoute] GetTimedGoal query, CancellationToken cancellationToken)
        {
            query.UserId = _currentUserId;
            return View("Edit", await _mediator.Send(query, cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> EditWeightlifting([FromRoute] GetWeightliftingGoal query, CancellationToken cancellationToken)
        {
            query.UserId = _currentUserId;
            return View("Edit", await _mediator.Send(query, cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> ViewTimed([FromRoute] GetTimedGoal query, CancellationToken cancellationToken)
        {
            query.UserId = _currentUserId;
            return View(await _mediator.Send(query, cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> ViewWeightlifting([FromRoute] GetWeightliftingGoal query, CancellationToken cancellationToken)
        {
            query.UserId = _currentUserId;
            return View(await _mediator.Send(query, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimed([FromForm] CreateTimedGoal command, CancellationToken cancellationToken)
        {
            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> CreateWeightlifting([FromForm] CreateWeightliftingGoal command, CancellationToken cancellationToken)
        {
            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTimed([FromForm] UpdateTimedGoal command, CancellationToken cancellationToken)
        {
            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWeightlifting([FromForm] UpdateWeightliftingGoal command, CancellationToken cancellationToken)
        {
            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }
    }
}