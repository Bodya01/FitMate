using Microsoft.AspNetCore.Mvc;

namespace YourFitnessTracker.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(string message) => View(message);
    }
}