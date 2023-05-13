using System;

namespace FitnessTracker.Models
{
    public class BodyweightRecord
    {
        public long ID { get; set; }
        public FitnessUser User { get; set; }
        public DateTime Date { get; set; }
        public float Weight { get; set; }

    }
}
