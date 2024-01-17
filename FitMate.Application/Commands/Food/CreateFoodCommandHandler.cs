using AutoMapper;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastucture.Dtos;
using FitMate.Infrastucture.Enums;
using MediatR;

namespace FitMate.Application.Commands.Food
{
    public record CreateFoodCommand(FoodDto Food) : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Carbohydrates { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int ServingSize { get; set; }
        public ServingUnit ServingUnit { get; set; }
        public string UserId { get; set; }
    }

    internal sealed class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand>
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
            var entity = new Infrastructure.Entities.Food
            {
                Id = request.Id,
                Name = request.Name,
                Calories = request.Calories,
                Carbohydrates = request.Carbohydrates,
                Fat = request.Fat,
                Protein = request.Protein,
                ServingSize = request.ServingSize,
                ServingUnit = request.ServingUnit,
            };

            if (entity.Id == Guid.Empty) await _unitOfWork.FoodRepository.Value.CreateAsync(entity, cancellationToken);
            else await _unitOfWork.FoodRepository.Value.UpdateAsync(entity, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}