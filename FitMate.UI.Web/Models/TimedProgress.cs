using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitMate.Models
{
    public class TimedProgress : GoalProgress
    {
        public float Quantity { get; set; }
        [NotMapped]
        public string QuantityUnit { get { return ((TimedGoal)this.Goal).QuantityUnit; } }
        public TimeSpan Time { get; set; }
    }
}
