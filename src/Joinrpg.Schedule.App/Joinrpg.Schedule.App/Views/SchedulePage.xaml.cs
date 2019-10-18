using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Joinrpg.Schedule.App.Models;
using Joinrpg.Schedule.App.Services.Interfaces;
using Joinrpg.Schedule.App.Views;
using Joinrpg.Schedule.App.ViewModels;
using Joinrpg.Web.XGameApi.Client;

namespace Joinrpg.Schedule.App.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class SchedulePage : ContentPage
    {
        private readonly ProgramItemsSelectMode _itemSelectMode;
        ScheduleViewModel viewModel;

        public SchedulePage() : this(ProgramItemsSelectMode.Today)
        {
            
        }
        public SchedulePage(ProgramItemsSelectMode itemSelectMode)
        {
            _itemSelectMode = itemSelectMode;
            InitializeComponent();

            var scheduleService = DependencyService.Get<CurrentScheduleHolder>();
            var provider = DependencyService.Get<IDateTimeProvider>();

            //TODO from DI
            BindingContext = viewModel = new ScheduleViewModel(scheduleService.GetCurrentScheduleService(), provider);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is ProgramItemModel item))
                return;

            var itemDetailViewModel = new ItemDetailViewModel();
            await itemDetailViewModel.InitializeAsync(item.Id);
            var itemDetailPage = new ItemDetailPage(itemDetailViewModel);
            await Navigation.PushAsync(itemDetailPage);

            // Manually deselect item.
            ItemsListView.SelectedItem = null; 
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!viewModel.Loaded)
            {
                await viewModel.InitializeAsync(_itemSelectMode);
            }
                
        }
    }
}