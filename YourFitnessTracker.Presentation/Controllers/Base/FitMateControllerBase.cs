using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace YourFitnessTracker.UI.Web.Controllers.Base
{
    [Authorize]
    public abstract class YourFitnessTrackerControllerBase : Controller
    {
        protected readonly IMediator _mediator;
        protected string _currentUserId;

        public YourFitnessTrackerControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void OnActionExecuting(ActionExecutingContext context) =>
            _currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}