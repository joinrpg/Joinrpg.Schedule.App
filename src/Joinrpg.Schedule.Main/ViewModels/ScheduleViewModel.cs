using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Joinrpg.Schedule.App.Services.Interfaces;
using JoinRpg.Web.XGameApi.Contract.Schedule;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Joinrpg.Schedule.Main.ViewModels
{
    public enum ProgramItemsSelectMode
    {
        Today,
        NowGoing,
        Tomorrow,
        All
    }

    public class ScheduleViewModel : BaseViewModel
    {
        private IDateTimeProvider _dateTimeProvider;
        private readonly IProjectScheduleService _scheduleService;
        private ObservableCollection<ProgramItemModel> _programItems = new ObservableCollection<ProgramItemModel>();
        private bool _loaded;
        private bool _hasError;
        private string _errorText;
        private Timer _timer;
        private string _currentTime;
        private IReadOnlyList<ProgramItemInfoApi> cachedItems;

        public Command LoadItemsCommand { get; set; }

        public ScheduleViewModel(CurrentScheduleHolder holder, IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            _scheduleService = holder.GetCurrentScheduleService();
            _timer = new Timer(UpdateTime);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        public void PageOnAppearing()
        {
            SelectItems();
            _timer.Change(0, 2000);
        }
        public void PageOnDisappearing()
        {
            _timer.Change(-1, -1);
        }

        private void UpdateTime(object state)
        {
            CurrentTime = _dateTimeProvider.Now().ToString("t");
        }

        private async Task ExecuteLoadItemsCommand()
        {
            await _scheduleService.ForceUpdate();
            await UpdateSchedule();
        }

        public string CurrentTime
        {
            get => _currentTime;
            set => SetProperty(ref _currentTime, value);
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

        //public string Title
        //{
        //    get => _title;
        //    set => SetProperty(ref _title, value);
        //}

        public override async Task InitializeAsync(object navigationData)
        {
            await base.InitializeAsync(navigationData);
            SelectMode = (ProgramItemsSelectMode)navigationData;
            Title = GetTitleFromMode(SelectMode);
            await UpdateSchedule();
        }

        private static string GetTitleFromMode(ProgramItemsSelectMode selectMode)
        {
            return selectMode switch
            {
                ProgramItemsSelectMode.Today => "Сегодня будут",
                ProgramItemsSelectMode.NowGoing => "Прямо сейчас",
                ProgramItemsSelectMode.Tomorrow => "Завтра будут",
                ProgramItemsSelectMode.All => "Все мероприятия",
                _ => throw new ArgumentOutOfRangeException(nameof(selectMode)),
            };
        }

        public ProgramItemsSelectMode SelectMode { get; set; }

        protected async Task UpdateSchedule()
        {
            try
            {
                cachedItems = await _scheduleService.GetSchedule();
                SelectItems();
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

        private void SelectItems()
        {
            ProgramItems.Clear();
            if (cachedItems is null)
            {
                return;
            }
            foreach (var item in SelectProgramItems(cachedItems))
            {
                ProgramItems.Add(new ProgramItemModel(item));
            }
        }

        private IOrderedEnumerable<ProgramItemInfoApi> SelectProgramItems(IReadOnlyList<ProgramItemInfoApi> items)
        {
            var now = _dateTimeProvider.Now();
            switch (SelectMode)
            {
                case ProgramItemsSelectMode.Today:
                    var today = now.Date;
                    return items.PlannedForDay(today).NotPassed(now)
                        .OrderBy(item => item.StartTime);
                case ProgramItemsSelectMode.NowGoing:
                    return items.NowGoing(now)
                        .OrderBy(item => item.Name);
                case ProgramItemsSelectMode.Tomorrow:
                    var tomorrow = now.Date.AddDays(1);
                    return items.PlannedForDay(tomorrow).OrderBy(item => item.StartTime);
                case ProgramItemsSelectMode.All:
                    return items.OrderBy(item => item.StartTime);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
