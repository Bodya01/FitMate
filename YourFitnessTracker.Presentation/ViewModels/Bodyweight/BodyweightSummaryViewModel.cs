﻿using System;
using System.Collections.Generic;
using System.Linq;
using YourFitnessTracker.Infrastructure.Extensions;
using YourFitnessTracker.Infrastucture.Dtos;
using YourFitnessTracker.Infrastucture.Dtos.Base;
using YourFitnessTracker.Presentation.Helpers;

namespace YourFitnessTracker.Presentation.ViewModels.Bodyweight
{
    public sealed class BodyweightSummaryViewModel
    {
        public BodyweightRecordDto MostRecentRecord { get; private set; }
        public BodyweightTargetDto Target { get; private set; }
        public TimeProgressViewModel CurrentWeek { get; private set; }
        public TimeProgressViewModel CurrentMonth { get; private set; }
        public TimeProgressViewModel AllTime { get; private set; }
        public TargetProgressViewModel TargetProgress { get; private set; }

        private BodyweightSummaryViewModel(
            BodyweightRecordDto mostRecentRecord,
            BodyweightTargetDto target,
            TimeProgressViewModel currentWeek,
            TimeProgressViewModel currentMonth,
            TimeProgressViewModel allTime,
            TargetProgressViewModel targetProgress)
        {
            MostRecentRecord = mostRecentRecord;
            Target = target;
            CurrentWeek = currentWeek;
            CurrentMonth = currentMonth;
            AllTime = allTime;
            TargetProgress = targetProgress;
        }

        public static BodyweightSummaryViewModel Create(IEnumerable<BodyweightRecordDto> allRecords, BodyweightTargetDto target)
        {
            var mostRecentRecord = allRecords.OrderByDescending(x => x.Date).FirstOrDefault();
            var firstRecord = allRecords.OrderBy(x => x.Date).FirstOrDefault();
            var currentMonthRecords = allRecords.Where(record => record.Date >= DateTime.Today.AddDays(-28));
            var currentWeekRecords = currentMonthRecords.Where(record => record.Date >= DateTime.Today.AddDays(-7));

            var currentMonth = BodyweightStatsCalculator.CalculateTimeProgress(currentMonthRecords, 28);
            var currentWeek = BodyweightStatsCalculator.CalculateTimeProgress(currentWeekRecords, 7);

            TimeProgressViewModel allTime;
            if (mostRecentRecord is not null)
            {
                allTime = BodyweightStatsCalculator.CalculateTimeProgress(allRecords, (mostRecentRecord.Date - firstRecord.Date).TotalDays.ToInt());
            }
            else
            {
                allTime = default;
            }

            TargetProgressViewModel targetProgress;
            if (target is not null)
            {
                targetProgress = BodyweightStatsCalculator.CalculateTargetProgress(target, mostRecentRecord);
            }
            else
            {
                targetProgress = default;
            }

            return new BodyweightSummaryViewModel(mostRecentRecord, target, currentWeek, currentMonth, allTime, targetProgress);
        }
    }
}