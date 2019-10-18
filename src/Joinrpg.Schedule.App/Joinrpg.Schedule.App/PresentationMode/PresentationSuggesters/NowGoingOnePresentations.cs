using System;
using System.Collections.Generic;
using Joinrpg.Schedule.App.Models;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.App.PresentationMode.PresentationSuggesters
{
    public class NowGoingOnePresentations : IPresentationSuggester
    {
        public IEnumerable<NavigationArgs> GetSuggestions(DateTimeOffset now, IReadOnlyList<ProgramItemInfoApi> schedule)
        {
            return schedule.NowGoing(now).NavigationArgs(MenuItemType.ProudlyPresent);
        }
    }
}