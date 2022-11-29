using System.ComponentModel;
using Joinrpg.Schedule.Main.ViewModels;

namespace Joinrpg.Schedule.Main.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class TomorrowPage : SchedulePageBase
    {
        public TomorrowPage(ScheduleViewModel scheduleViewModel) : base(scheduleViewModel)
        {
            InitializeComponent();
        }

        protected override ProgramItemsSelectMode ItemsSelectMode => ProgramItemsSelectMode.Tomorrow;
    }
}