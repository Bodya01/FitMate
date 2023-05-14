namespace FitMate.DAL.Entities
{
    public class Goal : IEntity
    {
        public long Id { get; set; }
        public FitnessUser User { get; set; }
        public string Activity { get; set; }
    }
}
