using AutoMapper;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos;
using MediatR;

namespace FitMate.Application.Queries.NutritionTarget
{
    public record GetCurrentNutritionTarget(string UserId) : IRequest<NutritionTargetDto>;

    internal sealed class GetCurrentNutritionTargetHandler : IRequestHandler<GetCurrentNutritionTarget, NutritionTargetDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCurrentNutritionTargetHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<NutritionTargetDto> Handle(GetCurrentNutritionTarget request, CancellationToken cancellationToken)
        {
            var userTarget = await _unitOfWork.NutritionTargetRepository.Value.GetTargetForUserAsync(request.UserId, cancellationToken);
            userTarget ??= new();

            return _mapper.Map<NutritionTargetDto>(userTarget);
        }
    }
}