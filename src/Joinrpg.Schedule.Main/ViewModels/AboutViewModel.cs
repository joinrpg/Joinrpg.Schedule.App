using System;
using System.Windows.Input;
using Joinrpg.Schedule.App.Services.Interfaces;
using Joinrpg.Schedule.Main.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Joinrpg.Schedule.Main.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Launcher.OpenAsync(new Uri("https://joinrpg.ru")));

            StartPresentationCommand = new Command(() => (Application.Current.MainPage as MainPage).PresentationMode = true);

            FreezeTimeDebugCommand = new Command(() => (DependencyService.Get<IDateTimeProvider>() as DateTimeProvider)?.SetDebug());
        }

        public ICommand OpenWebCommand { get; }

        public ICommand StartPresentationCommand { get; }

        public ICommand FreezeTimeDebugCommand { get; }
    }
}