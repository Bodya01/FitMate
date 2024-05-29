using YourFitnessTracker.Infrastucture.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YourFitnessTracker.Presentation.ViewModels.Nutrition
{
    public sealed class SummaryDataViewModel
    {
        public string Period { get; set; }
        public int Calories { get; set; }
        public int Carbs { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }

        public static SummaryDataViewModel CreateConcreteSummary(IEnumerable<FoodRecordDto> records, string period)
        {
            var viewModel = new SummaryDataViewModel
            {
                Period = period,
                Calories = CalculateSum(records, record => record.Food.Calories),
                Carbs = CalculateSum(records, record => record.Food.Carbohydrates),
                Protein = CalculateSum(records, record => record.Food.Protein),
                Fat = CalculateSum(records, record => record.Food.Fat)
            };

            return viewModel;
        }

        public static SummaryDataViewModel CreateAverageSummary(IEnumerable<IGrouping<DateTime, FoodRecordDto>> records, string period)
        {
            var viewModel = new SummaryDataViewModel
            {
                Period = period,
                Calories = CalculateAverage(records, grouping => grouping.Sum(record => record.Food.Calories)),
                Carbs = CalculateAverage(records, grouping => grouping.Sum(record => record.Food.Carbohydrates)),
                Protein = CalculateAverage(records, grouping => grouping.Sum(record => record.Food.Protein)),
                Fat = CalculateAverage(records, grouping => grouping.Sum(record => record.Food.Fat))
            };

            return viewModel;
        }

        private static int CalculateSum(IEnumerable<FoodRecordDto> records, Func<FoodRecordDto, int> selector) =>
            records.Sum(record => selector(record));

        private static int CalculateAverage(IEnumerable<IGrouping<DateTime, FoodRecordDto>> recordGroup, Func<IEnumerable<FoodRecordDto>, int> selector) =>
            !recordGroup.Any() ? 0 : (int)recordGroup.Average(grouping => selector(grouping));
    }
}