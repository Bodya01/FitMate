using YourFitnessTracker.Application.Commands.FoodRecord;
using FluentValidation;

namespace YourFitnessTracker.Presentation.Validators.Bodyweight
{
    public sealed class EditFoodRecordsValidator : AbstractValidator<EditFoodRecords>
    {
        public EditFoodRecordsValidator()
        {
            RuleFor(command => command.Quantities)
                .Must((command, quantities) => command.FoodIds?.Count == quantities?.Count)
                .WithMessage("FoodIds and Quantities must have the same count.");
        }
    }
}