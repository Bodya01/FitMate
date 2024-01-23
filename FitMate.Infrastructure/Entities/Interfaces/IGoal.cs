﻿namespace FitMate.Infrastructure.Entities.Interfaces;

// TODO: Review use of TPH and Discriminator
internal interface IGoal : IUserOwnedEntity
{
    public Guid Id { get; set; }

    public string Activity { get; set; }

    public string UserId { get; set; }

    public FitnessUser User { get; set; }
}