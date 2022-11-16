using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.Main
{
    public static class ProgramItemsFilters
    {
        public static IEnumerable<ProgramItemInfoApi> PlannedForDay(this IReadOnlyList<ProgramItemInfoApi> items, DateTime day)
        {
            return items.Where(item => item.PlannedForDay(day));
        }

        public static bool PlannedForDay(this ProgramItemInfoApi item, DateTime day)
        {
            return item.StartTime.Date == day || item.EndTime.Date == day;
        }

        public static IEnumerable<ProgramItemInfoApi> NowGoing(this IReadOnlyList<ProgramItemInfoApi> items, DateTimeOffset now)
        {
            return items.Where(item => item.NowGoing(now));
        }

        public static bool NowGoing(this ProgramItemInfoApi item, DateTimeOffset now)
        {
            return item.StartTime.AddMinutes(-10) < now && now < item.EndTime;
        }

        public static IEnumerable<ProgramItemInfoApi> NotPassed(this IEnumerable<ProgramItemInfoApi> items, DateTimeOffset now)
        {
            return items.Where(item => item.NotPassed(now));
        }

        public static bool NotPassed(this ProgramItemInfoApi item, DateTimeOffset now)
        {
            return item.EndTime > now;
        }
    }
}
