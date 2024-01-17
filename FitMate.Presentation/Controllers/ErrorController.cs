using Microsoft.AspNetCore.Mvc;

namespace FitMate.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(string message) => View(message);
    }
}