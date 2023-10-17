using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.Food
{
    public record DeleteFoodCommand(Guid Id) : IRequest;

    public class DeleteFoodCommandHandler : IRequestHandler<DeleteFoodCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public DeleteFoodCommandHandler(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.FoodRepository.Value.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null) throw new EntityNotFoundException($"Food with id {request.Id} was not found");

            _logger.LogInformation($"A deletion of food with {request.Id} id begun.");
            await _unitOfWork.FoodRepository.Value.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}