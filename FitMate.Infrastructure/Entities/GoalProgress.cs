﻿namespace FitMate.Infrastructure.Entities;

public class GoalProgress : IEntity
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public string UserId { get; set; }
    public Guid GoalId { get; set; }
    
    public FitnessUser User { get; set; }
    public Goal Goal { get; set; }
}