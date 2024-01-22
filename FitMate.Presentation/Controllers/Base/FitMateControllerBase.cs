using FitMate.Business.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitMate.UI.Web.Controllers.Base
{
    [Authorize]
    public abstract class FitMateControllerBase : Controller
    {
        protected readonly IUserService _userService;
        protected readonly IMediator _mediator;

        public FitMateControllerBase(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }
    }
}