using FitMate.Application.Commands.NutritionTarget;
using FitMate.Controllers;
using FitMate.Presentation.Helpers;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace FitMate.Presentation.Controllers
{
    public sealed class NutritionTargetController : FitMateControllerBase
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