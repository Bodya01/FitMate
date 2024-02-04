using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Business.Services.Base;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Extensions;
using FitMate.Infrastructure.Models.FoodRecord;
using FitMate.Infrastucture.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Business.Services
{
    internal sealed class FoodRecordService : ServiceBase, IFoodRecordService
    {
        public FoodRecordService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<IEnumerable<FoodRecordDto>> GetRecordsByDate(DateTime date, string userId, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.FoodRecordRepository.Value
                .Get(e => e.ConsumptionDate == date && e.UserId == userId, s => s)
                .ToListAsync(cancellationToken);

            foreach (var entity in entities)
            {
                await _unitOfWork.FoodRecordRepository.Value.LoadNavigationPropertyExplicitly(entity, r => r.Food, cancellationToken);
            }

            return _mapper.Map<IEnumerable<FoodRecordDto>>(entities);
        }

        public async Task<IEnumerable<FoodRecordDto>> GetRecordsForLastDays(uint previousDays, string userId, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.FoodRecordRepository.Value
                .Get(e => e.ConsumptionDate >= DateTime.Today.AddDays(-previousDays) && e.UserId == userId, s => s)
                .ToListAsync(cancellationToken);

            foreach (var entity in entities)
            {
                await _unitOfWork.FoodRecordRepository.Value.LoadNavigationPropertyExplicitly(entity, r => r.Food, cancellationToken);
            }

            return _mapper.Map<IEnumerable<FoodRecordDto>>(entities);
        }

        public async Task UpdateFoodRecordRangeAsync(IEnumerable<CreateFoodRecordModel> records, string userId, DateTime consumptionDate, CancellationToken cancellationToken = default)
        {
            var newRecords = _mapper.Map<IEnumerable<FoodRecord>>(records);
            var existingRecords = await _unitOfWork.FoodRecordRepository.Value
                .Get(e => e.UserId == userId && e.ConsumptionDate == consumptionDate, s => s)
                .ToListAsync(cancellationToken);

            await _unitOfWork.FoodRecordRepository.Value.DeleteRangeAsync(existingRecords, cancellationToken);
            await _unitOfWork.FoodRecordRepository.Value.CreateRangeAsync(newRecords, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}