using FitMate.Infrastructure.Entities;
using MediatR;

namespace FitMate.Handlers.Handlers.Bodyweight.Models.Requests
{
    public class EditBodyweightRecordsCommand : IRequest
    {
        public DateTime[] RecordDates { get; set; }
        public float[] recordWeights { get; set; }
        public FitnessUser User { get; set; }
    }
}
