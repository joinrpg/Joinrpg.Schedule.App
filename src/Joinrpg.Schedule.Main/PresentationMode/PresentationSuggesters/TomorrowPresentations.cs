using System;
using System.Collections.Generic;
using System.Linq;
using Joinrpg.Schedule.Main.Models;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.Main.PresentationMode.PresentationSuggesters
{
    public class TomorrowPresentations : IPresentationSuggester
    {
        public IEnumerable<NavigationArgs> GetSuggestions(DateTimeOffset now,
            IReadOnlyList<ProgramItemInfoApi> schedule)
        {
            if (now.TimeOfDay > new TimeSpan(18, 0, 0) && schedule.PlannedForDay(now.Date.AddDays(1)).Any())
            {
                yield return new NavigationArgs()
                {
                    Type = MenuItemType.Tomorrow,
                    Source = nameof(TomorrowPresentations),
                };
            }
        }
    }
}