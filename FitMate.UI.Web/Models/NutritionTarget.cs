using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{
    public class NutritionTarget
    {
        public long ID { get; set; }
        [Required]
        public FitnessUser User { get; set; }
        [Required]
        public int DailyCalories { get; set; }
        [Required]
        public int DailyCarbohydrates { get; set; }
        [Required]
        public int DailyProtein { get; set; }
        [Required]
        public int DailyFat { get; set; }

    }
}
