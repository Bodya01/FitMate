using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Business.Services.Base;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Models.BodyweightRecord;

namespace FitMate.Business.Services
{
    internal sealed class BodyweightRecordService : ServiceBase, IBodyweightRecordService
    {
        public BodyweightRecordService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

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
    }
}