using FitMate.Infrastucture.Dtos;
using System.Collections.Generic;

namespace FitMate.Presentation.ViewModels.Nutrition
{
    internal sealed class NewFoodViewModel
    {
        public IEnumerable<FoodDto> Foods { get; set; }
        public IEnumerable<FoodRecordDto> FoodRecords { get; set; }
    }
}