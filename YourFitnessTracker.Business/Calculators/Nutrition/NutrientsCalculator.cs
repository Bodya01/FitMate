using YourFitnessTracker.Business.Constants;
using YourFitnessTracker.Infrastructure.Extensions;

namespace YourFitnessTracker.Business.Calculators.Nutrition
{
    internal sealed class NutrientsCalculator
    {
        private readonly int _calories;

        public NutrientsCalculator(int calories)
        {
            _calories = calories;
        }

        public NutrientsModel Calculate()
        {
            return new NutrientsModel
            {
                Carbohydrates = CalculateNutrient(NutritionConstants.CarbohydratesPortion, NutritionConstants.RegularNutrientCalories),
                Proteins = CalculateNutrient(NutritionConstants.ProteinsPortion, NutritionConstants.RegularNutrientCalories),
                Fats = CalculateNutrient(NutritionConstants.FatsPortion, NutritionConstants.FatCalories)
            };
        }

        private int CalculateNutrient(double portion, double caloriesPerUnit) => (_calories / caloriesPerUnit * portion).ToInt();
    }
}

internal sealed class NutrientsModel
{
    public int Carbohydrates { get; set; }
    public int Proteins { get; set; }
    public int Fats { get; set; }
}