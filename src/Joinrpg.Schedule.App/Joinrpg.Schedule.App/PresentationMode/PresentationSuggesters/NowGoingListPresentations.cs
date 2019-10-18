using System;
using System.Collections.Generic;
using System.Linq;
using Joinrpg.Schedule.App.Models;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.App.PresentationMode.PresentationSuggesters
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