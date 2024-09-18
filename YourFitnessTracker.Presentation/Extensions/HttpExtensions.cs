using Microsoft.AspNetCore.Http;
using System;
using System.Web;
using YourFitnessTracker.Presentation.Controllers;
using YourFitnessTracker.Presentation.Helpers;

namespace YourFitnessTracker.Presentation.Extensions
{
    internal static class HttpExtensions
    {
        internal static readonly string ErrorControllerName = UiNamingHelper.GetControllerName<ErrorController>();
        internal const string InternalServerErrorName = nameof(ErrorController.InternalServerError);
        internal const string ForbidName = nameof(ErrorController.Forbidden);
        internal const string NotFoundName = nameof(ErrorController.NotFound);

        internal static void RedirectToInternalServerError(this HttpResponse response, string exceptionName, string message, string stackTrace, string traceId)
        {
            var encodedStackTrace = HttpUtility.UrlEncode(stackTrace);
            response.Redirect($"/{ErrorControllerName}/{InternalServerErrorName}?ExceptionName={exceptionName}&ExceptionMessage={Uri.EscapeDataString(message)}&StackTrace={encodedStackTrace}&RequestId={traceId}");
        }

        internal static void RedirectToForbid(this HttpResponse response, string message)
        {
            response.Redirect($"/{ErrorControllerName}/{ForbidName}?message={message}");
        }

        internal static void RedirectToNotFound(this HttpResponse response, string message)
        {
            response.Redirect($"/{ErrorControllerName}/{NotFoundName}?message={message}");
        }
    }
}