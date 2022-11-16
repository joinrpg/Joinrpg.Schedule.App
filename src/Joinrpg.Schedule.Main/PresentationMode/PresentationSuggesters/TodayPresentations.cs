using System;
using System.Collections.Generic;
using System.Linq;
using Joinrpg.Schedule.Main.Models;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.Main.PresentationMode.PresentationSuggesters
{
    public class TodayPresentations : IPresentationSuggester
    {
        public IEnumerable<NavigationArgs> GetSuggestions(DateTimeOffset now,
            IReadOnlyList<ProgramItemInfoApi> schedule)
        {
            if (schedule.PlannedForDay(now.Date).Any(item => item.StartTime.AddHours(1) > now))
            {
                yield return new NavigationArgs()
                {
                    Type = MenuItemType.Today,
                    Source = nameof(TodayPresentations),
                };
            }
        }
    }
}