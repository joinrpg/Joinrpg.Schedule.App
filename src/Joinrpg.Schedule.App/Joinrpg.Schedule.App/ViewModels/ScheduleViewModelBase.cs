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
    public abstract class ScheduleViewModelBase : BaseViewModel
    {
        private readonly IProjectScheduleService _scheduleService;
        private ObservableCollection<ProgramItemViewModel> _programItems = new ObservableCollection<ProgramItemViewModel>();
        private bool _loaded;
        private bool _hasError;
        private string _errorText;

        public Command LoadItemsCommand { get; set; }

        protected ScheduleViewModelBase()
        {

        }

        protected ScheduleViewModelBase(IProjectScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            await _scheduleService.ForceUpdate();
            await UpdateSchedule();
        }

        public ObservableCollection<ProgramItemViewModel> ProgramItems
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

        public override async Task InitializeAsync(object navigationData)
        {
            await base.InitializeAsync(navigationData);
            await UpdateSchedule();
        }

        protected async Task UpdateSchedule()
        {
            IReadOnlyList<ProgramItemInfoApi> items;
            try
            {
                items = await _scheduleService.GetSchedule();
            }
            catch (Exception e)
            {
                HasError = true;
                ErrorText = e.ToString();
                return;
            }

            ProgramItems.Clear();
            foreach (var item in SelectProgramItems(items))
            {
                ProgramItems.Add(new ProgramItemViewModel(item));
            }

            HasError = false;
            ErrorText = "";
            Loaded = true;
        }

        protected abstract IOrderedEnumerable<ProgramItemInfoApi> SelectProgramItems(IReadOnlyList<ProgramItemInfoApi> items);
    }
}
