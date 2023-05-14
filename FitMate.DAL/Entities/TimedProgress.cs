using System.ComponentModel.DataAnnotations.Schema;

namespace FitMate.DAL.Entities
{
    public class TimedProgress : GoalProgress, IEntity
    {
        public float Quantity { get; set; }
        [NotMapped]
        public string QuantityUnit { get { return ((TimedGoal)this.Goal).QuantityUnit; } }
        public TimeSpan Time { get; set; }
    }
}
