using System;
using System.Collections.Generic;
using Joinrpg.Schedule.Main.Models;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.Main.PresentationMode.PresentationSuggesters
{
    public class NowGoingOnePresentations : IPresentationSuggester
    {
        public IEnumerable<NavigationArgs> GetSuggestions(DateTimeOffset now, IReadOnlyList<ProgramItemInfoApi> schedule)
        {
            return schedule.NowGoing(now).NavigationArgs(MenuItemType.ProudlyPresent, nameof(NowGoingOnePresentations));
        }
    }
}