using FitMate.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitMate.ViewModels
{
    public class BodyweightSummaryViewModel
    {
        public IEnumerable<BodyweightRecord> AllRecords { get; set; }
        public IEnumerable<BodyweightRecord> CurrentWeekRecords { get; private set; }
        public IEnumerable<BodyweightRecord> CurrentMonthRecords { get; private set; }
        public BodyweightRecord MostRecentRecord { get; private set; }

        public BodyweightTarget Target { get; private set; }

        public float CurrentWeekProgress { get; private set; } = 0;
        public float CurrentWeekAverage { get; private set; } = 0;
        public float CurrentMonthProgress { get; private set; } = 0;
        public float CurrentMonthAverage { get; private set; } = 0;
        public float AllTimeProgress { get; private set; } = 0;
        public float AllTimeAverage { get; private set; } = 0;
        public float DistanceToTarget { get; private set; } = 0;
        public float DailyProgressNeeded { get; private set; } = 0;
        public float WeeklyProgressNeeded { get; private set; } = 0;

        public BodyweightSummaryViewModel(IEnumerable<BodyweightRecord> AllRecords, BodyweightTarget Target)
        {
            //if (AllRecords == null || AllRecords.Count() == 0)
            //    return;

            this.AllRecords = AllRecords;
            this.Target = Target;
            var asd = AllRecords.OrderByDescending(x => x.Date).ToList();
            this.MostRecentRecord = asd.FirstOrDefault();

            CurrentMonthRecords = AllRecords.Where(record => record.Date >= DateTime.Today.AddDays(-28));
            CurrentWeekRecords = CurrentMonthRecords.Where(record => record.Date >= DateTime.Today.AddDays(-7));

            if (CurrentWeekRecords.Count() != 0)
            {
                CurrentWeekProgress = CurrentWeekRecords.FirstOrDefault().Weight - CurrentWeekRecords.LastOrDefault().Weight;
                CurrentWeekAverage = CurrentWeekProgress / 7;
            }

            if (CurrentMonthRecords.Count() != 0)
            {
                CurrentMonthProgress = CurrentMonthRecords.FirstOrDefault().Weight - CurrentMonthRecords.LastOrDefault().Weight;
                CurrentMonthAverage = CurrentMonthProgress / 28;
            }

            if (AllRecords.Count() != 0)
            {
                AllTimeProgress = AllRecords.FirstOrDefault().Weight - AllRecords.LastOrDefault().Weight;
                AllTimeAverage = AllTimeProgress / ((float)(AllRecords.FirstOrDefault().Date - AllRecords.LastOrDefault().Date).TotalDays) * 7;
            }

            if (Target == null) return;

            DistanceToTarget = Target.TargetWeight - (MostRecentRecord is not null ? MostRecentRecord.Weight : 0);
            DailyProgressNeeded = (float)(DistanceToTarget / (Target.TargetDate - DateTime.Today).TotalDays);
            WeeklyProgressNeeded = DailyProgressNeeded * 7;
        }

    }
}
