﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Business.Services.Base;
using YourFitnessTracker.Core.UnitOfWork;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Exceptions.Bodyweight;
using YourFitnessTracker.Infrastructure.Models.BodyweightTarget;
using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Business.Services
{
    internal sealed class BodyweightTargetService : ServiceBase, IBodyweightTargetService
    {
        public BodyweightTargetService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<BodyweightTargetDto> GetCurrentTargetAsync(string userId, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.BodyweightTargetRepository.Value.Get(e => e.UserId == userId, s => s)
                .OrderByDescending(e => e.TargetDate)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity is null) throw new BodyweightTargetNotFoundException($"User with id {userId} does not have any bodyweight targets");

            return _mapper.Map<BodyweightTargetDto>(entity);
        }

        public async Task UpdateTargetAsync(UpdateBodyweightTargetModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.BodyweightTargetRepository.Value.Get(e => e.UserId == model.UserId, s => s)
                .FirstOrDefaultAsync(cancellationToken);

            entity ??= new();

            _mapper.Map(model, entity);

            if (!await CreateTargetIfNotExists(entity, cancellationToken))
            {
                await _unitOfWork.BodyweightTargetRepository.Value.UpdateAsync(entity, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        private async Task<bool> CreateTargetIfNotExists(BodyweightTarget entity, CancellationToken cancellationToken)
        {
            if (entity.Id != Guid.Empty) return false;

            await _unitOfWork.BodyweightTargetRepository.Value.CreateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
