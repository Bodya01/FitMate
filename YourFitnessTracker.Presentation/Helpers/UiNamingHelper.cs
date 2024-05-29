using YourFitnessTracker.UI.Web.Controllers.Base;

namespace YourFitnessTracker.Presentation.Helpers
{
    internal static class UiNamingHelper
    {
        public static string GetControllerName<T>() where T : YourFitnessTrackerControllerBase
        {
            return typeof(T).Name.Replace("Controller", string.Empty);
        }
    }
}