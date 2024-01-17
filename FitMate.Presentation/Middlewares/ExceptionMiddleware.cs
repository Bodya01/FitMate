using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using System;

namespace FitMate.Presentation.Middlewares
{
    internal sealed class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception occurred: {ex.Message}");

                var errorMessage = $"An exception has been thrown in a {context.Request.Method} operation. Details: {ex.Message}";
                context.Response.Redirect($"/Error?errorMessage={Uri.EscapeDataString(errorMessage)}");
            }
        }
    }

    internal static class MiddlewareExtensions
    {
        public static IServiceCollection RegisterCustomMiddlewares(this IServiceCollection services)
        {
            services.AddSingleton<ExceptionMiddleware>();
            return services;
        }

        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app) =>
            app.UseMiddleware<ExceptionMiddleware>();
    }
}
