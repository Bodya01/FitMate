using System;
using System.Collections.Generic;
using System.Linq;
using YourFitnessTracker.Infrastucture.Dtos;
using YourFitnessTracker.Infrastucture.Dtos.Base;
using YourFitnessTracker.Presentation.ViewModels.Bodyweight;

namespace YourFitnessTracker.Presentation.Helpers
{
    internal static class BodyweightStatsCalculator
    {
        public static TimeProgressViewModel CalculateTimeProgress(IEnumerable<BodyweightRecordDto> records, int days)
        {
            records = records ?? throw new ArgumentNullException(nameof(records));

            var result = new TimeProgressViewModel();

            if (records.Any())
            {
                result.Progress = records.Last().Weight - records.First().Weight;
                result.Average = days > 0 ? result.Progress / days : 0;
            }

            return result;
        }

        public static TargetProgressViewModel CalculateTargetProgress(BodyweightTargetDto target, BodyweightRecordDto mostRecentRecord)
        {
            var result = new TargetProgressViewModel();

            if (target is not null && mostRecentRecord is not null)
            {
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
            }

            return result;
        }
    }
}