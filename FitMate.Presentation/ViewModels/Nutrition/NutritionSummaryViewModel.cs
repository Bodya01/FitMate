using FitMate.Infrastucture.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitMate.Presentation.ViewModels.Nutrition
{
    internal sealed class NutritionSummaryViewModel
    {
        private readonly IEnumerable<FoodRecordDto> _records;

        public NutritionTargetDto Target { get; set; }

        public SummaryDataViewModel Today { get; set; }
        public SummaryDataViewModel Yesterday { get; set; }
        public SummaryDataViewModel WeekAverage { get; set; }
        public SummaryDataViewModel MonthAverage { get; set; }

        public static NutritionSummaryViewModel Create(IEnumerable<FoodRecordDto> records, NutritionTargetDto target) =>
            new NutritionSummaryViewModel(records, target);

        private NutritionSummaryViewModel(IEnumerable<FoodRecordDto> records, NutritionTargetDto target)
        {
            if (records is null) throw new ArgumentNullException(nameof(records), "There are no any food records!");

            if (target is null) throw new ArgumentNullException(nameof(target), "There is no nutrition target!");

            _records = records;
            Target = target;

            InitializeToday();
            InitializeYesterday();
            InitializeWeekAverage();
            InitializeMonthAverage();
        }

        private void InitializeToday()
        {
            Today = new();
            Today.Period = "Today";

            var todayRecords = _records.Where(record => record.ConsumptionDate == DateTime.Today);
            Today.Calories = CalculateSum(todayRecords, record => record.Food.Calories);
            Today.Carbs = CalculateSum(todayRecords, record => record.Food.Carbohydrates);
            Today.Protein = CalculateSum(todayRecords, record => record.Food.Protein);
            Today.Fat = CalculateSum(todayRecords, record => record.Food.Fat);
        }

        private void InitializeYesterday()
        {
            Yesterday = new();
            Yesterday.Period = "Yesterday";

            var yesterdayRecords = _records.Where(record => record.ConsumptionDate == DateTime.Today.AddDays(-1));
            Yesterday.Calories = CalculateSum(yesterdayRecords, record => record.Food.Calories);
            Yesterday.Carbs = CalculateSum(yesterdayRecords, record => record.Food.Carbohydrates);
            Yesterday.Protein = CalculateSum(yesterdayRecords, record => record.Food.Protein);
            Yesterday.Fat = CalculateSum(yesterdayRecords, record => record.Food.Fat);
        }

        private void InitializeWeekAverage()
        {
            WeekAverage = new();
            WeekAverage.Period = "Last 7 days";

            var weekRecordGroup = _records.Where(record => record.ConsumptionDate >= DateTime.Today.AddDays(-7)).GroupBy(record => record.ConsumptionDate);
            WeekAverage.Calories = CalculateAverage(weekRecordGroup, grouping => grouping.Sum(record => record.Food.Calories));
            WeekAverage.Carbs = CalculateAverage(weekRecordGroup, grouping => grouping.Sum(record => record.Food.Carbohydrates));
            WeekAverage.Protein = CalculateAverage(weekRecordGroup, grouping => grouping.Sum(record => record.Food.Protein));
            WeekAverage.Fat = CalculateAverage(weekRecordGroup, grouping => grouping.Sum(record => record.Food.Fat));
        }

        private void InitializeMonthAverage()
        {
            MonthAverage = new();
            MonthAverage.Period = "Last 28 days";

            var monthRecordGroup = _records.Where(record => record.ConsumptionDate >= DateTime.Today.AddDays(-28)).GroupBy(record => record.ConsumptionDate);
            MonthAverage.Calories = CalculateAverage(monthRecordGroup, grouping => grouping.Sum(record => record.Food.Calories));
            MonthAverage.Carbs = CalculateAverage(monthRecordGroup, grouping => grouping.Sum(record => record.Food.Carbohydrates));
            MonthAverage.Protein = CalculateAverage(monthRecordGroup, grouping => grouping.Sum(record => record.Food.Protein));
            MonthAverage.Fat = CalculateAverage(monthRecordGroup, grouping => grouping.Sum(record => record.Food.Fat));
        }

        private static int CalculateSum(IEnumerable<FoodRecordDto> records, Func<FoodRecordDto, int> selector) =>
            records.Sum(record => selector(record));

        private static int CalculateAverage(IEnumerable<IGrouping<DateTime, FoodRecordDto>> recordGroup, Func<IEnumerable<FoodRecordDto>, int> selector) =>
            !recordGroup.Any() ? 0 : (int)recordGroup.Average(grouping => selector(grouping));
    }
}