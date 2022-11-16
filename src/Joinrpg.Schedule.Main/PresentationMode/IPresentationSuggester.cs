using System;
using System.Collections.Generic;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.Main.PresentationMode
{
    public interface IPresentationSuggester
    {
        IEnumerable<NavigationArgs> GetSuggestions(DateTimeOffset now,
            IReadOnlyList<ProgramItemInfoApi> schedule);
    }
}