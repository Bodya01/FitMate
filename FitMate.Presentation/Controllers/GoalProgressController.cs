using FitMate.Application.Commands.GoalProgress;
using FitMate.Application.Queries.GoalProgress;
using FitMate.Business.Interfaces;
using FitMate.Controllers;
using FitMate.Presentation.Helpers;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace FitMate.Presentation.Controllers
{
    public class GoalProgressController : FitMateControllerBase
    {
        public GoalProgressController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<IActionResult> GetTimedProgress([FromHeader] GetTimedProgress query, CancellationToken cancellationToken)
        {
            query.UserId = _currentUserId;
            return Ok(await _mediator.Send(query, cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> GetWeightliftingProgress([FromHeader] GetWeightliftingProgress query, CancellationToken cancellationToken)
        {
            query.UserId = _currentUserId;
            return Ok(await _mediator.Send(query, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> AddTimedProgress([FromForm] CreateTimedProgress command, CancellationToken cancellationToken)
        {
            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(GoalController.ViewTimed), UiNamingHelper.GetControllerName<GoalController>(), new { Id = command.GoalId });
        }

        [HttpPost]
        public async Task<IActionResult> AddWeightliftingProgress([FromForm] CreateWeightliftingProgress command, CancellationToken cancellationToken)
        {
            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(GoalController.ViewWeightlifting), UiNamingHelper.GetControllerName<GoalController>(), new { Id = command.GoalId });
        }
    }
}