using System;
using System.Collections.Generic;
using System.Linq;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Presentation.ViewModels.Nutrition
{
    public sealed class NutritionSummaryViewModel
    {
        private readonly IEnumerable<FoodRecordDto> _records;

        public NutritionTargetDto Target { get; init; }
        public SummaryDataViewModel Today { get; init; }
        public SummaryDataViewModel Yesterday { get; init; }
        public SummaryDataViewModel WeekAverage { get; init; }
        public SummaryDataViewModel MonthAverage { get; init; }
        public NutritionCalculatorViewModel CalculatorViewModel { get; init; }

        public static NutritionSummaryViewModel Create(IEnumerable<FoodRecordDto> records, NutritionTargetDto target, int age, float? height, float? weight) =>
            new(records, target, age, height, weight);

        private NutritionSummaryViewModel(IEnumerable<FoodRecordDto> records, NutritionTargetDto target, int age, float? height, float? weight)
        {
            if (records is null) throw new ArgumentNullException(nameof(records), "There are no any food records!");
            target ??= new NutritionTargetDto(Guid.Empty, default, default, default, default, string.Empty);

            _records = records;
            Target = target;

            Today = CreateSummaryData(DateTime.Today, "Today");
            Yesterday = CreateSummaryData(DateTime.Today.AddDays(-1), "Yesterday");
            WeekAverage = CreateAverageSummaryData(DateTime.Today.AddDays(-7), "Last 7 days");
            MonthAverage = CreateAverageSummaryData(DateTime.Today.AddDays(-28), "Last 28 days");
            CalculatorViewModel = new NutritionCalculatorViewModel(age, height, weight);
        }

        private SummaryDataViewModel CreateSummaryData(DateTime date, string period)
        {
            var records = _records.Where(record => record.ConsumptionDate == date);
            return SummaryDataViewModel.CreateConcreteSummary(records, period);
        }

        private SummaryDataViewModel CreateAverageSummaryData(DateTime date, string period)
        {
            var records = _records.Where(record => record.ConsumptionDate >= date).GroupBy(record => record.ConsumptionDate);
            return SummaryDataViewModel.CreateAverageSummary(records, period);
        }
    }

    public sealed record NutritionCalculatorViewModel(int Age, float? Height, float? Weight);
}