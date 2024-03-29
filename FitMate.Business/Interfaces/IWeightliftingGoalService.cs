﻿using FitMate.Infrastructure.Models.Goal.Weightlifting;
using FitMate.Infrastucture.Dtos.Goals;

namespace FitMate.Business.Interfaces
{
    public interface IWeightliftingGoalService
    {
        Task<WeightliftingGoalDto> GetWeightliftingGoalAsync(Guid id, string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<WeightliftingGoalDto>> GetWeightliftingGoalsForUser(string userId, CancellationToken cancellationToken = default);
        Task CreateWeightliftingGoalAsync(CreateWeightliftingGoalModel model, CancellationToken cancellationToken = default);
        Task UpdateWeightliftingGoalAsync(UpdateWeightliftingGoalModel model, CancellationToken cancellationToken = default);
    }
}