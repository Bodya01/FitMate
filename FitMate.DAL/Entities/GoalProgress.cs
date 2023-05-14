namespace FitMate.DAL.Entities
{
    public class GoalProgress : IEntity
    {
        public long Id { get; set; }
        public FitnessUser User { get; set; }
        public Goal Goal { get; set; }
        public DateTime Date { get; set; }

    }
}
