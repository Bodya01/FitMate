using Microsoft.AspNetCore.Identity;

namespace FitMate.Infrastructure.Entities;

public class FitnessUser : IdentityUser, IEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }

    public ICollection<BodyweightRecord> BodyweightRecords { get; set; }
    public ICollection<BodyweightTarget> BodyweightTargets { get; set; }
    public ICollection<Goal> Goals { get; set; }
    public ICollection<GoalProgress> GoalProgressRecords { get; set; }
    public ICollection<NutritionTarget> NutritionTargets { get; set; }
    public ICollection<WorkoutPlan> WorkoutPlans { get; set; }
    public ICollection<FoodRecord> FoodRecords { get; set; }
}