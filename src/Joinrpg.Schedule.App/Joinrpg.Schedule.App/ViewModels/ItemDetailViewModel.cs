using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Joinrpg.Schedule.App.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ProgramItemModel Item { get; set; }



        public ItemDetailViewModel()
        {
            ScheduleHolder = DependencyService.Get<CurrentScheduleHolder>();
        }

        private CurrentScheduleHolder ScheduleHolder { get; set; }

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
