using AutoMapper;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos;
using MediatR;

namespace FitMate.Application.Commands.Food
{
    public record CreateFoodCommand(FoodDto Food) : IRequest;

    public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFoodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {
            var food = _mapper.Map<Infrastructure.Entities.Food>(request.Food);

            if (food.Id == Guid.Empty) await _unitOfWork.FoodRepository.Value.CreateAsync(food, cancellationToken);
            else await _unitOfWork.FoodRepository.Value.UpdateAsync(food, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}