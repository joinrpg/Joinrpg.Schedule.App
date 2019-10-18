using Joinrpg.Schedule.App.Models;

namespace Joinrpg.Schedule.App.PresentationMode
{
    public class NavigationArgs 
    {
        public MenuItemType Type { get; set; }
        public int? ExtraAgrument { get; set; }
        public string Source { get; set; }


        public override string ToString()
        {
            return $"{nameof(Type)}: {Type}, {nameof(ExtraAgrument)}: {ExtraAgrument}, {nameof(Source)}: {Source}";
        }
    }
}