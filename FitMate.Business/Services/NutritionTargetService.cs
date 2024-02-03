using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Business.Services.Base;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Business.Services
{
    internal sealed class NutritionTargetService : ServiceBase, INutritionTargetService
    {
        public NutritionTargetService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<NutritionTargetDto> GetTargetForUser(string userId, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.NutritionTargetRepository.Value
                .Get(e => e.UserId == userId, s => s)
                .SingleOrDefaultAsync(cancellationToken);

            return _mapper.Map<NutritionTargetDto>(entity);
        }
    }
}