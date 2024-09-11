using FluentValidation;
using YourFitnessTracker.Application.Commands.FoodRecord;

namespace YourFitnessTracker.Presentation.Validators.Nutrition
{
    public sealed class EditFoodRecordsValidator : AbstractValidator<EditFoodRecords>
    {
        public EditFoodRecordsValidator()
        {
            RuleFor(c => c.FoodIds).NotNull().WithMessage("Food is required");
            RuleFor(c => c.Quantities).NotNull().WithMessage("Food quantity is required");

            RuleFor(c => c.FoodIds).Must((c, x) =>
            {
                return c.FoodIds.Count != c.Quantities.Count;
            }).WithMessage("Something went wrong");
        }
    }
}