using AutoMapper;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Application.Queries.Food
{
    public record GetAllFoods() : IRequest<IEnumerable<FoodDto>>;

    internal sealed class GetAllFoodsHandler : IRequestHandler<GetAllFoods, IEnumerable<FoodDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllFoodsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FoodDto>> Handle(GetAllFoods request, CancellationToken cancellationToken)
        {
            var foods = await _unitOfWork.FoodRepository.Value.Get(e => true, s => s).ToListAsync(cancellationToken);
            return _mapper.Map<List<FoodDto>>(foods);
        }
    }
}