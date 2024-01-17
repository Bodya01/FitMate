using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Business.Services.Base;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Extensions;
using FitMate.Infrastructure.Models.FoodRecord;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Business.Services
{
    internal sealed class FoodRecordService : ServiceBase, IFoodRecordService
    {
        public FoodRecordService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task UpdateFoodRecordRangeAsync(IEnumerable<CreateFoodRecordModel> records, string userId, DateTime consumptionDate, CancellationToken cancellationToken = default)
        {
            if(records.IsNullOrEmpty()) throw new ArgumentNullException(nameof(records));

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