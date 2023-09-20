using FitMate.Infrastructure.Entities;
using MediatR;

namespace FitMate.Handlers.Handlers.Bodyweight.Models.Requests
{
    public class AddTodayWeightCommand : IRequest
    {
        public float Weight { get; set; }
        public FitnessUser User { get; set; }
    }
}
