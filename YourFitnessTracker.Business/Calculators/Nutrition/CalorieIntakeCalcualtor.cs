using YourFitnessTracker.Business.Constants;
using YourFitnessTracker.Infrastructure.Enums;
using YourFitnessTracker.Infrastructure.Extensions;

namespace YourFitnessTracker.Business.Calculators.Nutrition
{
    internal sealed class CalorieIntakeCalcualtor
    {
        private readonly float _weight;
        private readonly float _height;
        private readonly int _age;
        private readonly Genders _gender;
        private readonly ActivityLevels _activityLevel;

        public CalorieIntakeCalcualtor(float weight, float height, int age, Genders gender, ActivityLevels activityLevel)
        {
            _weight = weight;
            _height = height;
            _age = age;
            _gender = gender;
            _activityLevel = activityLevel;
        }

        public int Calculate()
        {
            var formulaResult = 10 * _weight + 6.25 * _height - 5 * _age + GetGenderMetabolicConstant();
            return MultiplyByActivity(formulaResult).ToInt();
        }

        private double MultiplyByActivity(double formulaResult)
        {
            var result = _activityLevel switch
            {
                ActivityLevels.Sedentary => formulaResult * NutritionConstants.ActivityMultipliers.Sedentary,
                ActivityLevels.Light => formulaResult * NutritionConstants.ActivityMultipliers.Light,
                ActivityLevels.Moderate => formulaResult * NutritionConstants.ActivityMultipliers.Moderate,
                ActivityLevels.Active => formulaResult * NutritionConstants.ActivityMultipliers.Active,
                ActivityLevels.VeryActive => formulaResult * NutritionConstants.ActivityMultipliers.VeryActive,
                ActivityLevels.ExtemelyActive => formulaResult * NutritionConstants.ActivityMultipliers.ExtemelyActive,
                _ => formulaResult * NutritionConstants.ActivityMultipliers.BMR
            };

            return result;
        }

        private int GetGenderMetabolicConstant() => _gender == Genders.Male ? 5 : -161;
    }
}