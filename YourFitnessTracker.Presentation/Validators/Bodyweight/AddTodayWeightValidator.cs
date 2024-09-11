using YourFitnessTracker.Applcation.Commands.Bodyweight;
using YourFitnessTracker.Presentation.Validators.Bodyweight.Base;

namespace YourFitnessTracker.Presentation.Validators.Bodyweight
{
    public sealed class AddTodayWeightValidator : BodyweightValidatorBase<AddTodayWeight>
    {
        public AddTodayWeightValidator()
        {
            ValidateBodyweight(c => c.Weight);
        }
    }
}