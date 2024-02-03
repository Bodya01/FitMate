using FitMate.Infrastucture.Dtos;
using System;
using System.Collections.Generic;

namespace FitMate.Presentation.ViewModels.Nutrition
{
    public sealed class NewFoodViewModel
    {
        public DateTime SelectedDate { get; set; }
        public IEnumerable<FoodDto> Foods { get; set; }
        public IEnumerable<FoodRecordDto> FoodRecords { get; set; }
    }
}