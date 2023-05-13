using System;

namespace FitMate.Models
{
    public class BodyweightTarget
    {
        public long ID { get; set; }
        public FitnessUser User { get; set; }
        public float TargetWeight { get; set; }
        public DateTime TargetDate { get; set; }
    }
}
