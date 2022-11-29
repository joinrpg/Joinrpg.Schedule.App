using System.ComponentModel;
using Joinrpg.Schedule.Main.ViewModels;

namespace Joinrpg.Schedule.Main.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public abstract class SchedulePageBase : ContentPage
    {
        protected abstract ProgramItemsSelectMode ItemsSelectMode { get; }

        readonly ScheduleViewModel viewModel;

        public SchedulePageBase(ScheduleViewModel scheduleViewModel)
        {
            BindingContext = viewModel = scheduleViewModel;
        }

        protected async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem is not ProgramItemModel item)
                return;

            await Shell.Current.GoToAsync($"item?id={item.Id}");

            // Manually deselect item.
            //ItemsListView.SelectedItem = null; 
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!viewModel.Loaded)
            {
                await viewModel.InitializeAsync(ItemsSelectMode);
            }

            viewModel.PageOnAppearing();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.PageOnDisappearing();
        }
    }
}