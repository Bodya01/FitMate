﻿using System.ComponentModel.DataAnnotations;

namespace FitMate.DAL.Entities
{
    public class WorkoutSession
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [Range(1, 28)]
        public int DayNumber { get; set; } = 1;

        public WorkoutActivity[] Activities { get; set; }
    }
}
