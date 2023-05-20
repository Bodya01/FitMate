using FitMate.DAL.Entities;
using MediatR;

namespace FitMate.Handlers.Handlers.Bodyweight.Models.Requests
{
    public class EditBodyweightRecordsCommand : IRequest
    {
        public DateTime[] RecordDates { get; set; }
        public float[] RecordWeights { get; set; }
        public FitnessUser User { get; set; }
    }
}
