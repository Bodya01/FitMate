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
            target ??= new NutritionTargetDto(Guid.Empty, default, default, default, default, string.Empty);

            _records = records;
            Target = target;

            Today = CreateSummaryData(DateTime.Today, "Today");
            Yesterday = CreateSummaryData(DateTime.Today.AddDays(-1), "Yesterday");
            WeekAverage = CreateAverageSummaryData(DateTime.Today.AddDays(-7), "Last 7 days");
            MonthAverage = CreateAverageSummaryData(DateTime.Today.AddDays(-28), "Last 28 days");
        }

        private SummaryDataViewModel CreateSummaryData(DateTime date, string period)
        {
            var records = _records.Where(record => record.ConsumptionDate == DateTime.Today);
            return SummaryDataViewModel.CreateConcreteSummary(records, period);
        }

        private SummaryDataViewModel CreateAverageSummaryData(DateTime date, string period)
        {
            var records = _records.Where(record => record.ConsumptionDate >= date).GroupBy(record => record.ConsumptionDate);
            return SummaryDataViewModel.CreateAverageSummary(records, period);
        }
    }
}