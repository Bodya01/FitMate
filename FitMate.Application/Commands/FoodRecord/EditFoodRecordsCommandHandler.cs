using FitMate.Core.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitMate.Application.Commands.FoodRecord
{
    public record EditFoodRecordsCommand(DateTime Date, List<Guid> FoodIds, List<float> Quantities) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class EditFoodRecordsCommandHandler : IRequestHandler<EditFoodRecordsCommand>
    {

        private readonly IUnitOfWork _unitOfWork;

        public EditFoodRecordsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EditFoodRecordsCommand request, CancellationToken cancellationToken)
        {
            var existingRecords = await _unitOfWork.FoodRecordRepository.Value
                .Get(e => e.UserId == request.UserId && e.ConsumptionDate == request.Date, s => s)
                .ToListAsync(cancellationToken);

            var newRecords = new Infrastructure.Entities.FoodRecord[request.FoodIds.Count];
            for (var i = 0; i < request.FoodIds.Count; i++)
            {
                newRecords[i] = new Infrastructure.Entities.FoodRecord()
                {
                    ConsumptionDate = request.Date,
                    UserId = request.UserId,
                    FoodId = request.FoodIds[i],
                    Quantity = request.Quantities[i]
                };
            }

            await _unitOfWork.FoodRecordRepository.Value.DeleteRangeAsync(existingRecords, cancellationToken);
            await _unitOfWork.FoodRecordRepository.Value.CreateRangeAsync(newRecords, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}