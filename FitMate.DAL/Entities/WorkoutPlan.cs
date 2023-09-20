using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace FitMate.DAL.Entities;

public class WorkoutPlan : IEntity
{
    public long Id { get; set; }

    public string Name { get; set; }
    public string SessionsJSON { get; set; }

    public string UserId { get; set; }
    public FitnessUser User { get; set; }

    [NotMapped]
    public ICollection<WorkoutSession> Sessions
    {
        get
        {
            if (string.IsNullOrEmpty(SessionsJSON))
                return new WorkoutSession[0];
            return JsonSerializer.Deserialize<WorkoutSession[]>(this.SessionsJSON);
        }
    }
}
