using FluentValidation;
using MediatR;
using System;
using System.Linq.Expressions;

namespace YourFitnessTracker.Presentation.Validators.Bodyweight.Base
{
    public abstract class BodyweightValidatorBase<T> : AbstractValidator<T> where T : IRequest
    {
        protected const float MinWeight = 10;
        protected const float MaxWeight = 650;

        protected virtual void ValidateBodyweight(Expression<Func<T, float>> expression)
        {
            RuleFor(expression).GreaterThanOrEqualTo(0).WithMessage($"Weight must be greater than {MinWeight}");
            RuleFor(expression).LessThanOrEqualTo(650).WithMessage($"Weight must be less than {MaxWeight}");
        }
    }
}