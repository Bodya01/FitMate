using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using MediatR;

namespace FitMate.Applcation.Commands.Bodyweight
{
    public record CreateTodayWeight(float Weight) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class CreateTodayWeightHandler : IRequestHandler<CreateTodayWeight>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTodayWeightHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateTodayWeight request, CancellationToken cancellationToken)
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