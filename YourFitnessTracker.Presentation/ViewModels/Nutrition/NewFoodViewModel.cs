using System;
using System.Collections.Generic;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Presentation.ViewModels.Nutrition
{
    public sealed class NewFoodViewModel
    {
        public DateTime SelectedDate { get; set; }
        public IEnumerable<FoodDto> Foods { get; set; }
        public IEnumerable<FoodRecordDto> FoodRecords { get; set; }
    }
}