using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using MediatR;

namespace FitMate.Applcation.Commands.Bodyweight
{
    public class AddTodayWeightCommand : IRequest
    {
        public float Weight { get; set; }
        public FitnessUser User { get; set; }
    }

    public class AddTodayWeightCommandHandler : IRequestHandler<AddTodayWeightCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTodayWeightCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddTodayWeightCommand request, CancellationToken cancellationToken)
        {
            var newRecord = new BodyweightRecord
            {
                User = request.User,
                Date = DateTime.Today,
                Weight = request.Weight
            };

            await _unitOfWork.BodyweightRecordRepository.Value.AddAsync(newRecord);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
