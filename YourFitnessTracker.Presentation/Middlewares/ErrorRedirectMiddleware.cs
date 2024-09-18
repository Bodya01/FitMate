using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using YourFitnessTracker.Infrastructure.Exceptions;
using YourFitnessTracker.Presentation.Extensions;

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
            }
            catch (EntityNotFoundException ex)
            {
                context.Response.RedirectToNotFound(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                context.Response.RedirectToForbid(ex.Message);
            }
            catch (Exception ex)
            {
                context.Response.RedirectToInternalServerError(ex.GetType().Name, ex.Message, ex.StackTrace, Activity.Current?.Id ?? context.TraceIdentifier);
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
