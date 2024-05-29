using YourFitnessTracker.Application.Commands.NutritionTarget;
using YourFitnessTracker.Controllers;
using YourFitnessTracker.Presentation.Helpers;
using YourFitnessTracker.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

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