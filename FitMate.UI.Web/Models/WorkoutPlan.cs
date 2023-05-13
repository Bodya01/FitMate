using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace FitMate.Models
{
    public class WorkoutPlan
    {
        public long ID { get; set; }

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
