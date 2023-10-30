using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.Goal
{
    public record DeleteGoalCommand(Guid Id) : IRequest;

    public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ILogger _logger;

        public DeleteGoalCommandHandler(IUnitOfWork unitOfWork/*, ILogger logger*/)
        {
            _unitOfWork = unitOfWork;
            //_logger = logger;
        }

        public async Task Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GoalRepository.Value.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null) throw new EntityNotFoundException($"Goal with id {request.Id} was not found");

            //_logger.LogInformation($"The deletion of goal with {request.Id} id begun");
            await _unitOfWork.GoalRepository.Value.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}