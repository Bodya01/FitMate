using FitMate.UI.Web.Controllers.Base;

namespace FitMate.Presentation.Helpers
{
    internal static class UiNamingHelper
    {
        public static string GetControllerName<T>() where T : FitMateControllerBase
        {
            return typeof(T).Name.Replace("Controller", string.Empty);
        }
    }
}