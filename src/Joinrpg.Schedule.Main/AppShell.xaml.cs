using Joinrpg.Schedule.Main.Views;

namespace Joinrpg.Schedule.Main
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("today", typeof(TodayPage));
            Routing.RegisterRoute("all", typeof(AllPage));
            Routing.RegisterRoute("now", typeof(NowPage));
            Routing.RegisterRoute("tomorrow", typeof(TomorrowPage));
            Routing.RegisterRoute("item", typeof(ItemDetailPage));
        }
    }
}