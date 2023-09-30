using FitMate.Core.Context;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace FitMate.UI.Web.Controllers.Base
{
    [Authorize]
    public class FitMateControllerBase : Controller
    {
        protected readonly FitMateContext _context;
        protected readonly UserManager<FitnessUser> _userManager;
        protected readonly IMediator _mediator;
        protected readonly IUnitOfWork _unitOfWork;

        public FitMateControllerBase(FitMateContext context, UserManager<FitnessUser> userManager, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _context = context;
            _userManager = userManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        [NonAction]
        protected async Task<FitnessUser> GetUserAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => _userManager.GetUserAsync(HttpContext.User), cancellationToken);
        }
        [NonAction]
        protected async Task<string> GetUserIdAsync(CancellationToken cancellationToken = default)
        {
            return (await Task.Run(() => _userManager.GetUserAsync(HttpContext.User), cancellationToken)).Id;
        }
    }
}
