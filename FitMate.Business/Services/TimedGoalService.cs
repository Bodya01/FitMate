﻿using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Business.Services.Base;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Exceptions;
using FitMate.Infrastructure.Extensions;
using FitMate.Infrastructure.Models.Goal.Timed;
using FitMate.Infrastucture.Dtos.Goals;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Business.Services
{
    internal sealed class TimedGoalService : ServiceBase, ITimedGoalService
    {
        public TimedGoalService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<TimedGoalDto> GetTimedGoal(Guid id, string userId, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.TimedGoalRepository.Value.GetByIdAsync(id, cancellationToken);

            if (entity is null) throw new EntityNotFoundException($"Timed goal with id {id} does not exist");

            CheckRestrictionsAccess(entity, id, userId);

            await _unitOfWork.TimedGoalRepository.Value.LoadNavigationCollectionExplicitly(entity, x => x.ProgressRecords, cancellationToken);

            return _mapper.Map<TimedGoalDto>(entity);
        }

        public async Task<IEnumerable<TimedGoalDto>> GetTimedGoalsForUser(string userId, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.TimedGoalRepository.Value.Get(e => e.UserId == userId, s => s)
                .ToListAsync(cancellationToken);

            if (entities.IsNullOrEmpty()) throw new EntityNotFoundException($"No weightlifting goals found for user {userId}");

            return _mapper.Map<IEnumerable<TimedGoalDto>>(entities);
        }

        public async Task CreateTimedGoalAsync(CreateTimedGoalModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<TimedGoal>(model);

            entity.Time = new TimeSpan(model.Hours, model.Minutes, model.Seconds);

            await _unitOfWork.TimedGoalRepository.Value.CreateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateTimedGoalAsync(UpdateTimedGoalModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.TimedGoalRepository.Value.GetByIdAsync(model.Id, cancellationToken);

            if (entity is null) throw new EntityNotFoundException($"Timed goal with id {model.Id} does not exist");

            CheckRestrictionsAccess(entity, model.Id, model.UserId ?? entity.UserId);

            _mapper.Map(model, entity);
            entity.Time = new TimeSpan(model.Hours, model.Minutes, model.Seconds);

            await _unitOfWork.TimedGoalRepository.Value.UpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}