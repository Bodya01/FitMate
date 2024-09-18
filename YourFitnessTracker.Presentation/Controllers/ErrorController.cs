using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourFitnessTracker.Presentation.ViewModels.Error;

namespace YourFitnessTracker.Presentation.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public IActionResult InternalServerError(ErrorViewModel model) => View("InternalServerError", model);
        public IActionResult NotFound(string message) => View("NotFound", message);
        public IActionResult Forbidden(string message) => View("Forbidden", message);
    }
}