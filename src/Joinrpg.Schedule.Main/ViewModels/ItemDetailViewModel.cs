using Joinrpg.Schedule.App.Services.Interfaces;

namespace Joinrpg.Schedule.Main.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public class ItemDetailViewModel : BaseViewModel
    {
        public ProgramItemModel? Item { get; set; }

        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                Task.Run(Update);
            }
        }

        public ItemDetailViewModel(CurrentScheduleHolder scheduleHolder, IDateTimeProvider dateTimeProvider)
        {
            ScheduleHolder = scheduleHolder;
            DateTimeProvider = dateTimeProvider;
        }
        private CurrentScheduleHolder ScheduleHolder { get; set; }

        private IDateTimeProvider DateTimeProvider { get; }

        public bool GoingNow => Item?.Item.NowGoing(DateTimeProvider.Now()) ?? false;
        public bool Today => !Passed && (Item?.Item.NowGoing(DateTimeProvider.Now().Date) ?? false);
        public bool Tomorrow => Item?.Item.PlannedForDay(DateTimeProvider.Now().Date.AddDays(1)) ?? false;

        public bool Passed => !Item?.Item.NotPassed(DateTimeProvider.Now()) ?? false;

        private void SetItemProperties(ProgramItemModel item)
        {
            Title = item?.Name;
            Item = item;
        }

        public async Task Update()
        {
            var schedule = await ScheduleHolder.GetCurrentScheduleService().GetSchedule();

            var item = schedule.First(i => i.ProgramItemId == Id);

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                SetItemProperties(new ProgramItemModel(item));
            });
        }
    }
}
