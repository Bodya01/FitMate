using FitMate.Infrastructure.Entities;
using FitMate.Infrastucture.Dtos;
using FitMate.Infrastucture.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitMate.ViewModels
{
    public class BodyweightSummaryViewModel
    {
        public IEnumerable<BodyweightRecordDto> AllRecords { get; set; }
        public IEnumerable<BodyweightRecordDto> CurrentWeekRecords { get; private set; }
        public IEnumerable<BodyweightRecordDto> CurrentMonthRecords { get; private set; }
        public BodyweightRecordDto MostRecentRecord { get; private set; }

        public BodyweightTargetDto Target { get; private set; }

        public float CurrentWeekProgress { get; private set; } = 0;
        public float CurrentWeekAverage { get; private set; } = 0;
        public float CurrentMonthProgress { get; private set; } = 0;
        public float CurrentMonthAverage { get; private set; } = 0;
        public float AllTimeProgress { get; private set; } = 0;
        public float AllTimeAverage { get; private set; } = 0;
        public float DistanceToTarget { get; private set; } = 0;
        public float DailyProgressNeeded { get; private set; } = 0;
        public float WeeklyProgressNeeded { get; private set; } = 0;

        public BodyweightSummaryViewModel(IEnumerable<BodyweightRecordDto> allRecords, BodyweightTargetDto target)
        {
            if (AllRecords is null || AllRecords.Any()) return;

            AllRecords = allRecords;
            Target = target;
            MostRecentRecord = allRecords.OrderByDescending(x => x.Date).ToList().FirstOrDefault();

            CurrentMonthRecords = allRecords.Where(record => record.Date >= DateTime.Today.AddDays(-28));
            CurrentWeekRecords = CurrentMonthRecords.Where(record => record.Date >= DateTime.Today.AddDays(-7));

            if (!CurrentWeekRecords.Any())
            {
                CurrentWeekProgress = CurrentWeekRecords.FirstOrDefault().Weight - CurrentWeekRecords.LastOrDefault().Weight;
                CurrentWeekAverage = CurrentWeekProgress / 7;
            }

            if (!CurrentMonthRecords.Any())
            {
                CurrentMonthProgress = CurrentMonthRecords.FirstOrDefault().Weight - CurrentMonthRecords.LastOrDefault().Weight;
                CurrentMonthAverage = CurrentMonthProgress / 28;
            }

            if (!allRecords.Any())
            {
                AllTimeProgress = allRecords.FirstOrDefault().Weight - allRecords.LastOrDefault().Weight;
                AllTimeAverage = AllTimeProgress / ((float)(allRecords.FirstOrDefault().Date - allRecords.LastOrDefault().Date).TotalDays) * 7;
            }

            if (target is null) return;

            DistanceToTarget = target.TargetWeight - (MostRecentRecord is not null ? MostRecentRecord.Weight : 0);
            DailyProgressNeeded = (float)(DistanceToTarget / (target.TargetDate - DateTime.Today).TotalDays);
            WeeklyProgressNeeded = DailyProgressNeeded * 7;
        }
    }
}