using Microsoft.AspNetCore.Mvc;

namespace YourFitnessTracker.Presentation.Helpers
{
    internal static class UiNamingHelper
    {
        public static string GetControllerName<T>() where T : ControllerBase =>
            typeof(T).Name.Replace("Controller", string.Empty);
    }
}