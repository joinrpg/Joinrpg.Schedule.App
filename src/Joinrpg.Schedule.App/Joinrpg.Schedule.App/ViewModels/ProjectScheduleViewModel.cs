using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoinRpg.Web.XGameApi.Contract.Schedule;
using Xamarin.Forms;

namespace Joinrpg.Schedule.App.ViewModels
{
    public class ProjectScheduleViewModel : BaseViewModel
    {
        private readonly IProjectScheduleWebClient _webClient;
        private ObservableCollection<ProgramItemViewModel> _programItems = new ObservableCollection<ProgramItemViewModel>();
        private bool _loaded;
        private bool _hasError;
        private string _errorText;

        public Command LoadItemsCommand { get; set; }

        public ProjectScheduleViewModel()
        {

        }

        public ProjectScheduleViewModel(IProjectScheduleWebClient webClient)
        {
            _webClient = webClient;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            Debug.Write("Load called");
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

        public bool IsBusy = false;

        public override async Task InitializeAsync(object navigationData)
        {
            await base.InitializeAsync(navigationData);
            List<ProgramItemInfoApi> items = null;
            try
            {
                items = await _webClient.GetSchedule(589);
            }
            catch (Exception e)
            {
                HasError = true;
                ErrorText = e.ToString();
                return;
            }

            ProgramItems.Clear();
            foreach (var item in items.OrderBy(item => item.StartTime))
            {
                ProgramItems.Add(new ProgramItemViewModel(item));
            }

            HasError = false;
            ErrorText = "";
            Loaded = true;
        }
    }
}
