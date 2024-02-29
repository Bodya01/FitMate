using FitMate.Infrastructure.Entities.Interfaces;
using FitMate.Infrastructure.Enums;
using Microsoft.AspNetCore.Identity;

namespace FitMate.Infrastructure.Entities;

public class FitnessUser : IdentityUser, IEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Genders Gender { get; set; }

    public ICollection<BodyweightRecord> BodyweightRecords { get; set; }
    public ICollection<BodyweightTarget> BodyweightTargets { get; set; }
    public ICollection<TimedGoal> TimedGoals { get; set; }
    public ICollection<WeightliftingGoal> WeightliftingGoals { get; set; }
    public ICollection<TimedProgress> TimedProgressRecords { get; set; }
    public ICollection<WeightliftingProgress> WeightliftingProgressRecords { get; set; }
    public ICollection<NutritionTarget> NutritionTargets { get; set; }
    public ICollection<WorkoutPlan> WorkoutPlans { get; set; }
    public ICollection<FoodRecord> FoodRecords { get; set; }
}