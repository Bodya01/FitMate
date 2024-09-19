using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourFitnessTracker.Presentation.ViewModels.Error;

namespace YourFitnessTracker.Presentation.Controllers
{
    [AllowAnonymous]
    public sealed class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult InternalServerError(ErrorViewModel model) => View("InternalServerError", model);

        [HttpGet]
        public IActionResult NotFound(string message) => View("NotFound", message);

        [HttpGet]
        public IActionResult Forbidden(string message) => View("Forbidden", message);
    }
}