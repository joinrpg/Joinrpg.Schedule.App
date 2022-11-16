using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Joinrpg.Schedule.Main.Models;
using Joinrpg.Schedule.App.Services.Interfaces;
using Joinrpg.Schedule.Main.Views;
using Joinrpg.Schedule.Main.ViewModels;
using Joinrpg.Web.XGameApi.Client;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Joinrpg.Schedule.Main.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    [QueryProperty(nameof(SelectMode), "mode")]
    public partial class SchedulePage : ContentPage, IQueryAttributable
    {
        private readonly ProgramItemsSelectMode _itemSelectMode;
        ScheduleViewModel viewModel;

        public string SelectMode { get; set; }

        

        public SchedulePage(ScheduleViewModel scheduleViewModel)
        {
            _itemSelectMode = ProgramItemsSelectMode.All;
            InitializeComponent();

            BindingContext = viewModel = scheduleViewModel;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem is not ProgramItemModel item)
                return;

            await Shell.Current.GoToAsync($"item?id={item.Id}");

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

            viewModel.PageOnAppearing();
                
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.PageOnDisappearing();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
    }
}