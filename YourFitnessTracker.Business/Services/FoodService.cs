using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Business.Services.Base;
using YourFitnessTracker.Core.UnitOfWork;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Exceptions;
using YourFitnessTracker.Infrastructure.Models.Food;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Business.Services
{
    internal sealed class FoodService : ServiceBase, IFoodService
    {
        public FoodService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<IEnumerable<FoodDto>> GetAllFoodsAsync(CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.FoodRepository.Value.Get(e => true, s => s)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<FoodDto>>(entities) ?? new List<FoodDto>();
        }

        public async Task<FoodDto> GetFoodByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.FoodRepository.Value.GetByIdAsync(id, cancellationToken);

            if (entity is null) throw new EntityNotFoundException($"Food with id {id} was not found");

            return _mapper.Map<FoodDto>(entity);
        }

        public async Task CreateFoodAsync(CreateFoodModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<Food>(model);

            await _unitOfWork.FoodRepository.Value.CreateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateFoodAsync(UpdateFoodModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.FoodRepository.Value.GetByIdAsync(model.Id, cancellationToken);

            if (entity is null) throw new EntityNotFoundException($"Food with id {model.Id} does not exist");

            _mapper.Map(model, entity);

            await _unitOfWork.FoodRepository.Value.UpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteFoodAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.FoodRepository.Value.GetByIdAsync(id, cancellationToken);

            if (entity is null) throw new EntityNotFoundException($"Food with id {id} does not exist");

            await _unitOfWork.FoodRepository.Value.DeleteAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}