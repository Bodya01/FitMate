using FitMate.Presentation.Validators.Bodyweight;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace FitMate.Presentation.Validators
{
    public static class ValidatorsExtensions
    {
        public static IServiceCollection RegisterFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<EditFoodRecordsValidator>();

            return services;
        }
    }
}