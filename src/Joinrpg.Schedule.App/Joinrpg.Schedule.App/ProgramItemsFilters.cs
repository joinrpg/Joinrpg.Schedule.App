using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.App
{
    public static class ProgramItemsFilters
    {
        public static IEnumerable<ProgramItemInfoApi> PlannedForDay(this IReadOnlyList<ProgramItemInfoApi> items, DateTime day)
        {
            return items.Where(item => item.StartTime.Date == day || item.EndTime.Date == day);
        }

        public static IEnumerable<ProgramItemInfoApi> NowGoing(this IReadOnlyList<ProgramItemInfoApi> items, DateTimeOffset now)
        {
            return items.Where(item => item.StartTime.AddMinutes(-10) < now && now < item.EndTime);
        }

        public static IEnumerable<ProgramItemInfoApi> NotPassed(this IEnumerable<ProgramItemInfoApi> items, DateTimeOffset now)
        {
            return items
                .Where(item => item.EndTime > now);
        }
    }
}
