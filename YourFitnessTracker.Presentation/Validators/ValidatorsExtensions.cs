using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using YourFitnessTracker.Presentation.Validators.Nutrition;

namespace YourFitnessTracker.Presentation.Validators
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