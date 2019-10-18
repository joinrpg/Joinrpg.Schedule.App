using System;
using System.Linq;
using System.Threading.Tasks;
using Joinrpg.Schedule.App.Services.Interfaces;
using Xamarin.Forms;

namespace Joinrpg.Schedule.App.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private string _goingNow;
        public ProgramItemModel Item { get; set; }



        public ItemDetailViewModel()
        {
            ScheduleHolder = DependencyService.Get<CurrentScheduleHolder>();
            DateTimeProvider = DependencyService.Get<IDateTimeProvider>();
        }

        private CurrentScheduleHolder ScheduleHolder { get; set; }
        private IDateTimeProvider DateTimeProvider { get; }

        public bool GoingNow => Item.Item.NowGoing(DateTimeProvider.Now());
        public bool Today => !Passed && Item.Item.NowGoing(DateTimeProvider.Now().Date);
        public bool Tomorrow => Item.Item.PlannedForDay(DateTimeProvider.Now().Date.AddDays(1));

        public bool Passed => !Item.Item.NotPassed(DateTimeProvider.Now());

        private void SetItemProperties(ProgramItemModel item)
        {
            Title = item?.Name;
            Item = item;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await base.InitializeAsync(navigationData);

            var schedule = await ScheduleHolder.GetCurrentScheduleService().GetSchedule();

            var item = schedule.First(i => i.ProgramItemId == (int) navigationData);
            SetItemProperties(new ProgramItemModel(item));
        }
    }
}
