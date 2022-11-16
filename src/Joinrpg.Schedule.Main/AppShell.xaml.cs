using Joinrpg.Schedule.Main.Views;

namespace Joinrpg.Schedule.Main
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("schedule", typeof(SchedulePage));
            Routing.RegisterRoute("item", typeof(ItemDetailPage));
        }
    }
}