using FitMate.Infrastructure.Entities;
using FitMate.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FitMate.UI.Web.Controllers.Base
{
    public class FitMateControllerBase : Controller
    {
        protected readonly FitMateContext _context;
        protected readonly UserManager<FitnessUser> _userManager;
        protected readonly IMediator _mediator;

        public FitMateControllerBase(FitMateContext context, UserManager<FitnessUser> userManager, IMediator mediator)
        {
            _context = context;
            _userManager = userManager;
            _mediator = mediator;
        }

        [NonAction]
        protected async Task<FitnessUser> GetUserAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
        [NonAction]
        protected async Task<string> GetUserIdAsync()
        {
            return (await _userManager.GetUserAsync(HttpContext.User)).Id;
        }
    }
}
