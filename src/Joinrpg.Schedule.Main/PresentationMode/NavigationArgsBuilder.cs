using System.Collections.Generic;
using System.Linq;
using Joinrpg.Schedule.Main.Models;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.Main.PresentationMode
{
    internal static class NavigationArgsBuilder
    {
        public static IEnumerable<NavigationArgs> NavigationArgs(this IEnumerable<ProgramItemInfoApi> items, MenuItemType menuItemType, string source)
        {
            return items.Select(item => new NavigationArgs()
            {
                Type = menuItemType,
                ExtraAgrument = item.ProgramItemId,
                Source = source,
            });
        }
    }
}