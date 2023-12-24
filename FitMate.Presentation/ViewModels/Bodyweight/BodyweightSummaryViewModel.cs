using FitMate.Infrastucture.Dtos;
using FitMate.Infrastucture.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitMate.Presentation.ViewModels.Bodyweight
{
    public class BodyweightSummaryViewModel
    {
        private readonly IEnumerable<BodyweightRecordDto> _allRecords;
        private readonly IEnumerable<BodyweightRecordDto> _currentWeekRecords;
        private readonly IEnumerable<BodyweightRecordDto> _currentMonthRecords;

        public BodyweightRecordDto MostRecentRecord { get; private set; }
        public BodyweightTargetDto Target { get; private set; }
        public TimeProgressViewModel CurrentWeek { get; private set; }
        public TimeProgressViewModel CurrentMonth { get; private set; }
        public TimeProgressViewModel AllTime { get; private set; }
        public TargetProgressViewModel TargetProgress { get; private set; }

        public static BodyweightSummaryViewModel Create(IEnumerable<BodyweightRecordDto> allRecords, BodyweightTargetDto target) =>
            new BodyweightSummaryViewModel(allRecords, target);

        private BodyweightSummaryViewModel(IEnumerable<BodyweightRecordDto> allRecords, BodyweightTargetDto target)
        {
            _allRecords = allRecords;
            _currentMonthRecords = _allRecords.Where(record => record.Date >= DateTime.Today.AddDays(-28));
            _currentWeekRecords = _currentMonthRecords.Where(record => record.Date >= DateTime.Today.AddDays(-7));


            MostRecentRecord = _allRecords.OrderByDescending(x => x.Date).ToList().FirstOrDefault();
            Target = target;

            InitializeAllTime();
            InitializeCurrentMonth();
            InitializeCurrentWeek();
            InitializeTargetProgress();
        }

        private void InitializeAllTime()
        {
            AllTime = new();

            if (_allRecords.Any())
            {
                AllTime.Progress = _allRecords.FirstOrDefault().Weight - _allRecords.LastOrDefault().Weight;
                AllTime.Average = AllTime.Progress / (float)(_allRecords.FirstOrDefault().Date - _allRecords.LastOrDefault().Date).TotalDays * 7;
            }
        }

        private void InitializeCurrentMonth()
        {
            CurrentMonth = new();

            if (_currentMonthRecords.Any())
            {
                CurrentMonth.Progress = _currentMonthRecords.FirstOrDefault().Weight - _currentMonthRecords.LastOrDefault().Weight;
                CurrentMonth.Average = CurrentMonth.Progress / 28;
            }
        }

        private void InitializeCurrentWeek()
        {
            CurrentWeek = new();

            if (_currentWeekRecords.Any())
            {
                CurrentWeek.Progress = _currentWeekRecords.FirstOrDefault().Weight - _currentWeekRecords.LastOrDefault().Weight;
                CurrentWeek.Average = CurrentWeek.Progress / 7;
            }
        }

        private void InitializeTargetProgress()
        {
            TargetProgress = new();

            if (Target is null) return;

            TargetProgress.Distance = Target.TargetWeight - (MostRecentRecord is not null ? MostRecentRecord.Weight : 0);
            TargetProgress.RequiredDailyProgress = (float)(TargetProgress.Distance / (Target.TargetDate - DateTime.Today).TotalDays);
            TargetProgress.RequiredWeeklyProgress = TargetProgress.RequiredDailyProgress * 7;
        }
    }
}