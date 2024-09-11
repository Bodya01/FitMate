using FluentValidation;
using System;
using System.Linq;
using YourFitnessTracker.Applcation.Commands.Bodyweight;
using YourFitnessTracker.Presentation.Validators.Bodyweight.Base;

namespace YourFitnessTracker.Presentation.Validators.Bodyweight
{
    public class EditBodyweightRecordsValidator : BodyweightValidatorBase<EditBodyweightRecords>
    {
        public EditBodyweightRecordsValidator()
        {
            RuleFor(c => c.Weights).NotNull().WithMessage("Weights are required");
            RuleFor(c => c.Dates).NotNull().WithMessage("Dates are required");

            RuleFor(c => c.Weights).Must((c, x) => {
                return (c.Weights.Length == c.Dates.Length) || c.Weights.Any(x => x >= MinWeight && x <= MaxWeight);
            }).WithMessage("Something went wrong");
        }
    }
}