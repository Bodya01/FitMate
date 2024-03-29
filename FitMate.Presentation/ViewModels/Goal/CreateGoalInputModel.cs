﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FitMate.Presentation.ViewModels.Goal
{
    public sealed class CreateGoalInputModel
    {
        [Required]
        [MaxLength(30)]
        public string Activity { get; set; }
        [Required]
        [MaxLength(20)]
        public string Type { get; set; }
        [Range(0, 500)]
        public float Weight { get; set; }
        [Range(0, 100)]
        public int Reps { get; set; }
        [Range(0, 24)]
        public int Hours { get; set; }
        [Range(0, 60)]
        public int Minutes { get; set; }
        [Range(0, 60)]
        public int Seconds { get; set; }
        [Range(0, double.MaxValue)]
        public float Quantity { get; set; }
        [MaxLength(30)]
        public string QuantityUnit { get; set; }
    }
}