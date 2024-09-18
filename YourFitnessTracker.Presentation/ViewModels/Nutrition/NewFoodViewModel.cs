using System;
using System.Collections.Generic;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Presentation.ViewModels.Nutrition
{
    internal sealed class NewFoodViewModel
    {
        public DateTime SelectedDate { get; set; }
        public IEnumerable<FoodDto> Foods { get; set; }
        public IEnumerable<FoodRecordDto> FoodRecords { get; set; }

        public NewFoodViewModel() { }

        public NewFoodViewModel(DateTime selectedDate, IEnumerable<FoodDto> foods, IEnumerable<FoodRecordDto> foodRecords)
        {
            SelectedDate = selectedDate;
            Foods = foods;
            FoodRecords = foodRecords;
        }
    }
}