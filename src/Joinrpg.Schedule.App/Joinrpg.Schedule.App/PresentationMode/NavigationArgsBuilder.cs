using System.Collections.Generic;
using System.Linq;
using Joinrpg.Schedule.App.Models;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.App.PresentationMode
{
    internal static class NavigationArgsBuilder
    {
        public static NavigationArgs ToNavigationArgs(this ProgramItemInfoApi item, MenuItemType menuItemType)
        {
            return new NavigationArgs()
            {
                Type = menuItemType,
                ExtraAgrument = item.ProgramItemId,
            };
        }

        public static IEnumerable<NavigationArgs> NavigationArgs(this IEnumerable<ProgramItemInfoApi> items, MenuItemType menuItemType)
        {
            return items.Select(item => ToNavigationArgs(item, menuItemType));
        }
    }
}