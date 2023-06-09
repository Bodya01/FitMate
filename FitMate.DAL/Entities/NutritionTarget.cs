﻿using System.ComponentModel.DataAnnotations;

namespace FitMate.DAL.Entities
{
    public class NutritionTarget : IEntity
    {
        public long Id { get; set; }
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
