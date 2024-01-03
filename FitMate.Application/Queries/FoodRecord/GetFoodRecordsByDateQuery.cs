using AutoMapper;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Application.Queries.FoodRecord
{
    public record GetFoodRecordsByDateQuery(string UserId, DateTime ConsumptionDate) : IRequest<List<FoodRecordDto>>;

    internal sealed class GetFoodRecordsByDateQueryHanlder : IRequestHandler<GetFoodRecordsByDateQuery, List<FoodRecordDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFoodRecordsByDateQueryHanlder(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<FoodRecordDto>> Handle(GetFoodRecordsByDateQuery request, CancellationToken cancellationToken)
        {
            var userRecords = await _unitOfWork.FoodRecordRepository.Value
                .Get(e => e.UserId == request.UserId && e.ConsumptionDate.Date == request.ConsumptionDate.Date, s => s)
                .ToListAsync(cancellationToken);

            foreach (var record in userRecords)
            {
                await _unitOfWork.FoodRecordRepository.Value.LoadNavigationPropertyExplicitly(record, r => r.Food, cancellationToken);
            }

            return _mapper.Map<List<FoodRecordDto>>(userRecords);
        }
    }
}