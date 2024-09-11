using FluentValidation;
using System;
using YourFitnessTracker.Application.Commands.BodyweightTarget;
using YourFitnessTracker.Presentation.Validators.Bodyweight.Base;

namespace YourFitnessTracker.Presentation.Validators.Bodyweight
{
    public sealed class EditBodyweightTargetValidator : BodyweightValidatorBase<EditBodyweightTarget>
    {
        public EditBodyweightTargetValidator()
        {
            ValidateBodyweight(c => c.Weight);
            RuleFor(c => c.Date).GreaterThan(DateTime.Today).WithMessage("Date must be not earlier than tomorrow");
        }
    }
}