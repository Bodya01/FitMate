using AutoMapper;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos;
using MediatR;

namespace FitMate.Application.Queries.Food
{
    public record GetFoodQuery(Guid FoodId) : IRequest<FoodDto>;

    public class GetFoodQueryHandler : IRequestHandler<GetFoodQuery, FoodDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFoodQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FoodDto> Handle(GetFoodQuery request, CancellationToken cancellationToken)
        {
            var food = await _unitOfWork.FoodRepository.Value.GetByIdAsync(request.FoodId, cancellationToken);
            return _mapper.Map<FoodDto>(food);
        }
    }
}