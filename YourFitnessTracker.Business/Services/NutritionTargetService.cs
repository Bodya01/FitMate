using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourFitnessTracker.Business.Calculators.Nutrition;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Business.Services.Base;
using YourFitnessTracker.Core.UnitOfWork;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Models.NutritionTarget;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Business.Services
{
    internal sealed class NutritionTargetService : ServiceBase, INutritionTargetService
    {
        public NutritionTargetService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<NutritionTargetDto> GetUserTargetAsync(string userId, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.NutritionTargetRepository.Value
                .Get(e => e.UserId == userId, s => s)
                .SingleOrDefaultAsync(cancellationToken);

            return _mapper.Map<NutritionTargetDto>(entity);
        }

        public async Task SetUserTargetAsync(NutritionTargetCalculationParameters calculationParameters, string userId, CancellationToken cancellationToken = default)
        {
            var calorieCalculator = new CalorieIntakeCalcualtor(
                calculationParameters.Weight,
                calculationParameters.Height,
                calculationParameters.Age,
                calculationParameters.Gender,
                calculationParameters.ActivityLevel);

            var calories = calorieCalculator.Calculate();

            var nutritionCalculator = new NutrientsCalculator(calories);

            var nutrients = nutritionCalculator.Calculate();

            var targetModel = new CreateNutritionTargetModel(calories, nutrients.Carbohydrates, nutrients.Proteins, nutrients.Fats, userId);

            if (!await CreateTargetIfNotExistsAsync(targetModel, cancellationToken))
            {
                await UpdateTargetAsync(targetModel, cancellationToken);
            }
        }

        private async Task<bool> CreateTargetIfNotExistsAsync(CreateNutritionTargetModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.NutritionTargetRepository.Value
                .Get(e => e.UserId == model.UserId, s => s)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity is not null) return false;

            var target = _mapper.Map<NutritionTarget>(model);

            await _unitOfWork.NutritionTargetRepository.Value.CreateAsync(target, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        private async Task UpdateTargetAsync(CreateNutritionTargetModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.NutritionTargetRepository.Value
                .Get(e => e.UserId == model.UserId, s => s)
                .SingleAsync(cancellationToken);

            _mapper.Map(model, entity);

            await _unitOfWork.NutritionTargetRepository.Value.UpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}