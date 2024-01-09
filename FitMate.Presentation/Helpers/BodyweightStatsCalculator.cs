﻿using FitMate.Infrastucture.Dtos.Base;
using FitMate.Infrastucture.Dtos;
using FitMate.Presentation.ViewModels.Bodyweight;
using System.Collections.Generic;
using System;
using System.Linq;

namespace FitMate.Presentation.Helpers
{
    internal static class BodyweightStatsCalculator
    {
        public static TimeProgressViewModel CalculateTimeProgress(IEnumerable<BodyweightRecordDto> records, int days)
        {
            records = records ?? throw new ArgumentNullException(nameof(records));

            var result = new TimeProgressViewModel();

            if (records.Any())
            {
                result.Progress = records.First().Weight - records.Last().Weight;
                result.Average = days > 0 ? result.Progress / days : 0;
            }

            return result;
        }

        public static TargetProgressViewModel CalculateTargetProgress(BodyweightTargetDto target, BodyweightRecordDto mostRecentRecord)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));
            mostRecentRecord = mostRecentRecord ?? throw new ArgumentNullException(nameof(mostRecentRecord));

            var result = new TargetProgressViewModel();

            result.Distance = target.TargetWeight - mostRecentRecord.Weight;

            var daysDifference = (target.TargetDate - DateTime.Today).TotalDays;

            if (daysDifference > 0)
            {
                result.RequiredDailyProgress = (float)(result.Distance / daysDifference);
                result.RequiredWeeklyProgress = result.RequiredDailyProgress * 7;
            }
            else
            {
                result.RequiredDailyProgress = 0;
                result.RequiredWeeklyProgress = 0;
            }

            return result;
        }
    }
}