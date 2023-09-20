using FitMate.Infrastructure.Entities;
using FitMate.Data;
using FitMate.Handlers.Handlers.Bodyweight.Models.Requests;
using MediatR;

namespace FitMate.Handlers.Handlers.Bodyweight.Commands
{
    public class AddTodayWeightCommandHandler : IRequestHandler<AddTodayWeightCommand>
    {
        private readonly IBodyweightRepository _bodyweightRepository;

        public AddTodayWeightCommandHandler(IBodyweightRepository bodyweightRepository)
        {
            _bodyweightRepository = bodyweightRepository;
        }

        public async Task Handle(AddTodayWeightCommand request, CancellationToken cancellationToken)
        {
            var newRecord = new BodyweightRecord()
            {
                User = request.User,
                Date = DateTime.Today,
                Weight = request.Weight
            };

            await _bodyweightRepository.StoreBodyweightRecord(newRecord);
        }
    }
}
