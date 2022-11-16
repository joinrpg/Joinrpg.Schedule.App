using System;
using System.Collections.Generic;
using System.Text;

namespace Joinrpg.Schedule.Main.Models
{
    public enum MenuItemType
    {
        Today,
        About,
        NowGoing,
        Tomorrow,
        ProudlyPresent,
        All,
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
