using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Joinrpg.Schedule.Main.Models;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.Main.PresentationMode.PresentationSuggesters
{
    public class ProudlyPresentsPresentation : IPresentationSuggester
    {
        public IEnumerable<NavigationArgs> GetSuggestions(DateTimeOffset now, IReadOnlyList<ProgramItemInfoApi> schedule)
        {
            return schedule.NotPassed(now).NavigationArgs(MenuItemType.ProudlyPresent, nameof(ProudlyPresentsPresentation));
        }
    }
}