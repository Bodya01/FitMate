using FitMate.Infrastructure.Entities;
using FitMate.Infrastucture.Dtos;
using System.Collections.Generic;

namespace FitMate.Presentation.ViewModels.Nutrition
{
    public sealed class NewFoodViewModel
    {
        public IEnumerable<FoodDto> Foods { get; set; }
        public IEnumerable<FoodRecordDto> FoodRecords { get; set; }
    }
}