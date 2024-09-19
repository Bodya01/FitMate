using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using YourFitnessTracker.Application.Commands.NutritionTarget;
using YourFitnessTracker.Presentation.Controllers.Base;
using YourFitnessTracker.Presentation.Helpers;

namespace YourFitnessTracker.Presentation.Controllers
{
    public sealed class NutritionTargetController : YourFitnessTrackerControllerBase
    {
        public NutritionTargetController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> CalculateTarget(SetNutritionTarget command, CancellationToken cancellationToken)
        {
            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(NutritionController.Summary), UiNamingHelper.GetControllerName<NutritionController>());
        }
    }
}