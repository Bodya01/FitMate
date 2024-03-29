﻿using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Business.Services.Base;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Exceptions;
using FitMate.Infrastructure.Extensions;
using FitMate.Infrastructure.Models.Goal.Weightlifting;
using FitMate.Infrastucture.Dtos.Goals;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Business.Services
{
    internal sealed class WeightliftingGoalService : ServiceBase, IWeightliftingGoalService
    {
        public WeightliftingGoalService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<WeightliftingGoalDto> GetWeightliftingGoalAsync(Guid id, string userId, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.WeightliftingGoalRepository.Value.GetByIdAsync(id, cancellationToken);

            if (entity is null) throw new EntityNotFoundException($"Weightlifting goal with id {id} does not exist");

            CheckRestrictionsAccess(entity, id, userId);

            await _unitOfWork.WeightliftingGoalRepository.Value.LoadNavigationCollectionExplicitly(entity, x => x.ProgressRecords, cancellationToken);

            return _mapper.Map<WeightliftingGoalDto>(entity);
        }

        public async Task<IEnumerable<WeightliftingGoalDto>> GetWeightliftingGoalsForUser(string userId, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.WeightliftingGoalRepository.Value.Get(e => e.UserId == userId, s => s)
                .ToListAsync(cancellationToken);

            if (entities.IsNullOrEmpty()) throw new EntityNotFoundException($"No weightlifting goals found for user {userId}");

            return _mapper.Map<IEnumerable<WeightliftingGoalDto>>(entities);
        }

        public async Task CreateWeightliftingGoalAsync(CreateWeightliftingGoalModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<WeightliftingGoal>(model);

            await _unitOfWork.WeightliftingGoalRepository.Value.CreateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateWeightliftingGoalAsync(UpdateWeightliftingGoalModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.WeightliftingGoalRepository.Value.GetByIdAsync(model.Id, cancellationToken);

            if (entity is null) throw new EntityNotFoundException($"Weightlifting goal with id {model.Id} does not exist");

            CheckRestrictionsAccess(entity, model.Id, model.UserId ?? entity.UserId);

            _mapper.Map(model, entity);
            await _unitOfWork.WeightliftingGoalRepository.Value.UpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}