using System;
using System.Collections.Generic;
using System.Linq;
using Joinrpg.Schedule.Main.Models;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.Main.PresentationMode.PresentationSuggesters
{
    public class NowGoingListPresentations : IPresentationSuggester
    {
        public IEnumerable<NavigationArgs> GetSuggestions(DateTimeOffset now, IReadOnlyList<ProgramItemInfoApi> schedule)
        {
            if (schedule.NowGoing(now).Any())
            {
                yield return new NavigationArgs()
                {
                    Type = MenuItemType.NowGoing,
                    Source = nameof(NowGoingListPresentations),
                };
            }
        }
    }
}