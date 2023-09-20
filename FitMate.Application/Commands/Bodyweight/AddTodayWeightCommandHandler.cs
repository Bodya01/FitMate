using FitMate.Data;
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
        private readonly IBodyweightRepository _bodyweightRepository;

        public AddTodayWeightCommandHandler(IBodyweightRepository bodyweightRepository)
        {
            _bodyweightRepository = bodyweightRepository;
        }

        public async Task Handle(AddTodayWeightCommand request, CancellationToken cancellationToken)
        {
            var newRecord = new BodyweightRecord
            {
                User = request.User,
                Date = DateTime.Today,
                Weight = request.Weight
            };

            await _bodyweightRepository.StoreBodyweightRecord(newRecord);
        }
    }
}
