using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Business.Services.Base;
using YourFitnessTracker.Core.UnitOfWork;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Exceptions.Bodyweight;
using YourFitnessTracker.Infrastructure.Extensions;
using YourFitnessTracker.Infrastructure.Models.BodyweightRecord;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Business.Services
{
    internal sealed class BodyweightRecordService : ServiceBase, IBodyweightRecordService
    {
        public BodyweightRecordService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<IEnumerable<BodyweightRecordDto>> GetAllRecordsAsync(string userId, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.BodyweightRecordRepository.Value.Get(e => e.UserId == userId, s => s)
                .ToListAsync(cancellationToken);

            if (entities.IsNullOrEmpty()) throw new BodyweightRecordNotFoundException($"User with id {userId} does not have any bodyweight records");

            return _mapper.Map<IEnumerable<BodyweightRecordDto>>(entities);
        }

        public async Task<IEnumerable<BodyweightRecordDto>> GetRecordsByDateAsync(DateTime from, DateTime to, string userId, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.BodyweightRecordRepository.Value.Get(e =>
                e.UserId == userId &&
                e.Date >= from &&
                e.Date <= to,
                s => s)
                .OrderByDescending(e => e.Date)
                .ToListAsync(cancellationToken);

            if (entities.IsNullOrEmpty()) throw new BodyweightRecordNotFoundException($"User with id {userId} does not have any bodyweight records between dates from {from} to {to}");

            return _mapper.Map<IEnumerable<BodyweightRecordDto>>(entities);
        }

        public async Task<BodyweightRecordDto?> GetLastRecordAsync(string userId, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.BodyweightRecordRepository.Value.Get(e => e.UserId == userId, s => s)
                .OrderByDescending(e => e.Date)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity is null) return null; // TODO: Replace with an exception

            return _mapper.Map<BodyweightRecordDto>(entity);
        }

        public async Task CreateTodayRecordAsync(CreateTodayBodyweightRecordModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<BodyweightRecord>(model);

            entity.Date = DateTime.Today;

            await _unitOfWork.BodyweightRecordRepository.Value.CreateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateBodyweightRecordAsync(CreateBodyweightRecordModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<BodyweightRecord>(model);

            await _unitOfWork.BodyweightRecordRepository.Value.CreateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRangeAsync(IEnumerable<UpdateBodyweightRecordModel> records, string userId, CancellationToken cancellationToken = default)
        {
            var existingEntities = await _unitOfWork.BodyweightRecordRepository.Value
                .Get(e => e.UserId == userId, s => s)
                .ToListAsync(cancellationToken);

            var entities = _mapper.Map<IEnumerable<BodyweightRecord>>(records);

            await _unitOfWork.BodyweightRecordRepository.Value.DeleteRangeAsync(existingEntities, cancellationToken);
            await _unitOfWork.BodyweightRecordRepository.Value.CreateRangeAsync(entities, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}