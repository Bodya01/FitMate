using FitMate.Infrastructure.Entities;
using System.Collections.Generic;

namespace FitMate.Presentation.ViewModels.Nutrition
{
    internal sealed class NewFoodViewModel
    {
        public List<Food> UserFoods { get; set; }
        public List<FoodRecord> FoodRecords { get; set; }
    }
}