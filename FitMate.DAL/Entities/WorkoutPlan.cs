using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace FitMate.DAL.Entities
{
    public class WorkoutPlan : IEntity
    {
        public long Id { get; set; }

        [Required]
        public FitnessUser User { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SessionsJSON { get; set; }

        [NotMapped]
        public WorkoutSession[] Sessions
        {
            get
            {
                if (string.IsNullOrEmpty(SessionsJSON))
                    return new WorkoutSession[0];
                return JsonSerializer.Deserialize<WorkoutSession[]>(this.SessionsJSON);
            }
        }
    }
}
