using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Business.Services.Base;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Exceptions;
using FitMate.Infrastructure.Extensions;
using FitMate.Infrastructure.Models.BodyweightRecord;
using FitMate.Infrastucture.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Business.Services
{
    internal sealed class BodyweightRecordService : ServiceBase, IBodyweightRecordService
    {
        public BodyweightRecordService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<IEnumerable<BodyweightRecordDto>> GetAllRecordsAsync(string userId, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.BodyweightRecordRepository.Value.Get(e => e.UserId == userId, s => s)
                .ToListAsync(cancellationToken);

            if (entities.IsNullOrEmpty()) throw new EntityNotFoundException($"User with id {userId} does not have any bodyweight records");

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

            if (entities.IsNullOrEmpty())
                throw new EntityNotFoundException($"User with id {userId} does not have any bodyweight records between dates from {from} to {to}");

            return _mapper.Map<IEnumerable<BodyweightRecordDto>>(entities);
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