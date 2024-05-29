using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using YourFitnessTracker.Infrastructure.Exceptions;
using YourFitnessTracker.Presentation.Controllers;
using YourFitnessTracker.Presentation.Helpers;

namespace YourFitnessTracker.Presentation.Middlewares
{
    internal sealed class ErrorRedirectMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorRedirectMiddleware> _logger;

        public ErrorRedirectMiddleware(ILogger<ErrorRedirectMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);

                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.Redirect($"/{UiNamingHelper.GetControllerName<ErrorController>()}/{nameof(ErrorController.NotFound)}");
                }
            }
            catch (ForbiddenException ex)
            {
                context.Response.Redirect("/Identity/Account/AccessDenied");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception occurred: {ex.GetType().Name}\n{ex.Message}");

                var errorMessage = $"An exception has been thrown in a {context.Request.Method} operation. Details: {ex.Message}";
                context.Response.Redirect($"/{UiNamingHelper.GetControllerName<ErrorController>()}/{nameof(ErrorController.InternalServerError)}?ExceptionName={ex.GetType().Name}&ExceptionMessage={ex.Message}&RequestId={Activity.Current?.Id ?? context.TraceIdentifier}");
            }
        }
    }

    internal static class MiddlewareExtensions
    {
        public static IServiceCollection RegisterCustomMiddlewares(this IServiceCollection services)
        {
            services.AddSingleton<ErrorRedirectMiddleware>();
            return services;
        }

        public static IApplicationBuilder UseErrorRedirectMiddleware(this IApplicationBuilder app) =>
            app.UseMiddleware<ErrorRedirectMiddleware>();
    }
}
