namespace FitMate.Models
{
    public class Goal
    {
        public long ID { get; set; }
        public FitnessUser User { get; set; }
        public string Activity { get; set; }
    }
}
