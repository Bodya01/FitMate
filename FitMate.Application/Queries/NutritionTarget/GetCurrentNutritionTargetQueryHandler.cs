using AutoMapper;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos;
using MediatR;

namespace FitMate.Application.Queries.NutritionTarget
{
    public record GetCurrentNutritionTargetQuery(string UserId) : IRequest<NutritionTargetDto>;
    internal sealed class GetCurrentNutritionTargetQueryHandler : IRequestHandler<GetCurrentNutritionTargetQuery, NutritionTargetDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCurrentNutritionTargetQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<NutritionTargetDto> Handle(GetCurrentNutritionTargetQuery request, CancellationToken cancellationToken)
        {
            var userTarget = await _unitOfWork.NutritionTargetRepository.Value.GetTargetForUserAsync(request.UserId, cancellationToken);
            userTarget ??= new();

            return _mapper.Map<NutritionTargetDto>(userTarget);
        }
    }
}