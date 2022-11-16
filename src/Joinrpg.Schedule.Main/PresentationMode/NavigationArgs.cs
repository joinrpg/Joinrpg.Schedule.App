using Joinrpg.Schedule.Main.Models;

namespace Joinrpg.Schedule.Main.PresentationMode
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