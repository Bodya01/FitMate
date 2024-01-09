using FitMate.Business.Interfaces;
using FitMate.Core.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitMate.UI.Web.Controllers.Base
{
    // TODO: Remove injection of temporary objects
    [Authorize]
    public abstract class FitMateControllerBase : Controller
    {
        protected readonly IUserService _userService;
        protected readonly IMediator _mediator;
        protected readonly IUnitOfWork _unitOfWork;

        public FitMateControllerBase(IMediator mediator, IUnitOfWork unitOfWork, IUserService userService)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
    }
}