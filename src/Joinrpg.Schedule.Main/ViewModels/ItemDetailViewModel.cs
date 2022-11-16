using Joinrpg.Schedule.App.Services.Interfaces;

namespace Joinrpg.Schedule.Main.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public class ItemDetailViewModel : BaseViewModel
    {
        public ProgramItemModel? Item { get => item; set { SetProperty(ref item, value); } }

        private int id;
        private bool goingNow;
        private bool today;
        private bool tomorrow;
        private bool passed;
        private ProgramItemModel? item;

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

        public bool GoingNow
        {
            get => goingNow;
            set
            {
                SetProperty(ref goingNow, value);
            }
        }
        public bool Today { get => today; set { SetProperty(ref today, value); } }
        public bool Tomorrow { get => tomorrow; set { SetProperty(ref tomorrow, value); } }
        public bool Passed { get => passed; set { SetProperty(ref passed, value); } }

        private void SetItemProperties(ProgramItemModel item)
        {
            Title = item.Name;
            Item = item;
            GoingNow = item.Item.NowGoing(DateTimeProvider.Now());
            Passed = item.Item.NotPassed(DateTimeProvider.Now());
            Today = !Passed && Item.Item.NowGoing(DateTimeProvider.Now().Date);
            Tomorrow = item.Item.PlannedForDay(DateTimeProvider.Now().Date.AddDays(1));
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
