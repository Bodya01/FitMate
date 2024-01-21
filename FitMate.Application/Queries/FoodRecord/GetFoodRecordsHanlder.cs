using AutoMapper;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Application.Queries.FoodRecord
{
    public record GetFoodRecords(string UserId, uint PreviousDays) : IRequest<List<FoodRecordDto>>;
    internal sealed class GetFoodRecordsHanlder : IRequestHandler<GetFoodRecords, List<FoodRecordDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFoodRecordsHanlder(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<FoodRecordDto>> Handle(GetFoodRecords request, CancellationToken cancellationToken)
        {
            var userRecords = await _unitOfWork.FoodRecordRepository.Value
                .Get(e => e.UserId == request.UserId && e.ConsumptionDate >= DateTime.Today.AddDays(-request.PreviousDays), s => s)
                .ToListAsync(cancellationToken);

            foreach (var record in userRecords)
            {
                await _unitOfWork.FoodRecordRepository.Value.LoadNavigationPropertyExplicitly(record, r => r.Food, cancellationToken);
            }

            return _mapper.Map<List<FoodRecordDto>>(userRecords);
        }
    }
}