using System.ComponentModel;

namespace YourFitnessTracker.Infrastructure.Enums
{
    public enum ActivityLevels
    {
        [Description("Basal Metabolic Rate (BMR)")]
        BMR,

        [Description("Sedentary: little or no exercise")]
        Sedentary,

        [Description("Light: exercise 1-3 times/week")]
        Light,

        [Description("Moderate: exercise 4-5 times/week")]
        Moderate,

        [Description("Active: daily exercise or intense exercise 3-4 times/week")]
        Active,

        [Description("Very Active: intense exercise 6-7 times/week")]
        VeryActive,

        [Description("Active: very intense exercise, or physical job")]
        ExtemelyActive
    }
}