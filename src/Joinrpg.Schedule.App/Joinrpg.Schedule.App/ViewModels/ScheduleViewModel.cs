using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Joinrpg.Schedule.App.Services.Interfaces;
using JoinRpg.Web.XGameApi.Contract.Schedule;
using Xamarin.Forms;

namespace Joinrpg.Schedule.App.ViewModels
{
    public enum ProgramItemsSelectMode
    {
        Today,
        NowGoing,
        Tomorrow,
    }

    public class ScheduleViewModel : BaseViewModel
    {
        private IDateTimeProvider _dateTimeProvider;
        private readonly IProjectScheduleService _scheduleService;
        private ObservableCollection<ProgramItemModel> _programItems = new ObservableCollection<ProgramItemModel>();
        private bool _loaded;
        private bool _hasError;
        private string _errorText;
        private string _title;

        public Command LoadItemsCommand { get; set; }

        public ScheduleViewModel()
        {

        }

        public ScheduleViewModel(IProjectScheduleService scheduleService, IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            _scheduleService = scheduleService;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            await _scheduleService.ForceUpdate();
            await UpdateSchedule();
        }

        public ObservableCollection<ProgramItemModel> ProgramItems
        {
            get => _programItems;
            set => SetProperty(ref _programItems, value);
        }

        public bool Loaded
        {
            get => _loaded;
            set => SetProperty(ref _loaded, value);
        }

        public bool HasError
        {
            get => _hasError;
            set => SetProperty(ref _hasError, value);
        }

        public string ErrorText
        {
            get => _errorText;
            set => SetProperty(ref _errorText, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await base.InitializeAsync(navigationData);
            SelectMode = (ProgramItemsSelectMode) navigationData;
            switch (SelectMode)
            {
                case ProgramItemsSelectMode.Today:
                    Title = "Сегодня будут";
                    break;
                case ProgramItemsSelectMode.NowGoing:
                    Title = "Прямо сейчас";
                    break;
                case ProgramItemsSelectMode.Tomorrow:
                    Title = "Завтра будут";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            await UpdateSchedule();
        }

        public ProgramItemsSelectMode SelectMode { get; set; }

        protected async Task UpdateSchedule()
        {
            try
            {
                var items = await _scheduleService.GetSchedule();
                ProgramItems.Clear();
                foreach (var item in SelectProgramItems(items))
                {
                    ProgramItems.Add(new ProgramItemModel(item));
                }
            }
            catch (Exception e)
            {
                HasError = true;
                ErrorText = e.ToString();
                return;
            }



            HasError = false;
            ErrorText = "";
            Loaded = true;
        }

        private IOrderedEnumerable<ProgramItemInfoApi> SelectProgramItems(IReadOnlyList<ProgramItemInfoApi> items)
        {
            var now = _dateTimeProvider.Now();
            switch (SelectMode)
            {
                case ProgramItemsSelectMode.Today:
                    var today = now.Date;
                    return items.Where(item => item.StartTime.Date == today || item.EndTime.Date == today)
                        .Where(item => item.EndTime > now)
                        .OrderBy(item => item.StartTime);
                case ProgramItemsSelectMode.NowGoing:
                    return items.Where(item => item.StartTime.AddMinutes(-10) < now && now < item.EndTime)
                        .OrderBy(item => item.Name);
                case ProgramItemsSelectMode.Tomorrow:
                    var tomorrow = now.Date.AddDays(1);
                    return items.Where(item => item.StartTime.Date == tomorrow || item.EndTime.Date == tomorrow)
                        .OrderBy(item => item.StartTime);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
