using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using MediatR;

namespace FitMate.Applcation.Commands.Bodyweight
{
    public record AddTodayWeight(float Weight) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class AddTodayWeightHandler : IRequestHandler<AddTodayWeight>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTodayWeightHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddTodayWeight request, CancellationToken cancellationToken)
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