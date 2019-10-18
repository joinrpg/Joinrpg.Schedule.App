using System;
using System.Windows.Input;
using Joinrpg.Schedule.App.Services.Interfaces;
using Joinrpg.Schedule.App.Views;
using Xamarin.Forms;

namespace Joinrpg.Schedule.App.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://joinrpg.ru")));

            StartPresentationCommand = new Command(() => (Application.Current.MainPage as MainPage).PresentationMode = true);

            FreezeTimeDebugCommand = new Command(() => (DependencyService.Get<IDateTimeProvider>() as DateTimeProvider)?.SetDebug());
        }

        public ICommand OpenWebCommand { get; }

        public ICommand StartPresentationCommand { get; }

        public ICommand FreezeTimeDebugCommand { get; }
    }
}