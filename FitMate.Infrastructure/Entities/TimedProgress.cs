﻿using System.ComponentModel.DataAnnotations.Schema;
using FitMate.Infrastructure.Entities.Interfaces;

namespace FitMate.Infrastructure.Entities;

public class TimedProgress : GoalProgress, IEntity
{
    public float Quantity { get; set; }
    public TimeSpan Time { get; set; }


    [NotMapped]
    public string QuantityUnit { get { return ((TimedGoal)this.Goal).QuantityUnit; } }
}
