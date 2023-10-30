using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using MediatR;

namespace FitMate.Applcation.Commands.Bodyweight
{
    public record CreateTodayWeightCommand(float Weight) : IRequest
    {
        public string UserId { get; set; }
    }

    public class CreateTodayWeightCommandHandler : IRequestHandler<CreateTodayWeightCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTodayWeightCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateTodayWeightCommand request, CancellationToken cancellationToken)
        {
            var newRecord = new BodyweightRecord
            {
                UserId = request.UserId,
                Date = DateTime.Today,
                Weight = request.Weight
            };

            await _unitOfWork.BodyweightRecordRepository.Value.CreateAsync(newRecord, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}