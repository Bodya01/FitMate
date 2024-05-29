using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Business.Services.Base;
using YourFitnessTracker.Core.UnitOfWork;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Models.FoodRecord;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Business.Services
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