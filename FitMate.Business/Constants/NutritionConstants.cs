namespace FitMate.Business.Constants
{
    internal static class NutritionConstants
    {
        public const int RegularNutrientCalories = 4;

        public const int FatCalories = 9;

        public const double CarbohydratesPortion = 0.5;

        public const double ProteinsPortion = 0.2;

        public const double FatsPortion = 0.3;

        public static class ActivityMultipliers
        {
            public const float BMR = 1f;
            public const float Sedentary = 1.2f;
            public const float Light = 1.375f;
            public const float Moderate = 1.465f;
            public const float Active = 1.55f;
            public const float VeryActive = 1.725f;
            public const float ExtemelyActive = 1.9f;
        }
    }
}