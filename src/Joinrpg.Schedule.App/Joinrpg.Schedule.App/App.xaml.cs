using System;
using Joinrpg.Schedule.App.PresentationMode;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Joinrpg.Schedule.App.Services;
using Joinrpg.Schedule.App.Views;
using Joinrpg.Schedule.Services.Impl;

namespace Joinrpg.Schedule.App
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<ScheduleServiceImpl>();
            DependencyService.Register<DateTimeProvider>();
            DependencyService.Register<CurrentScheduleHolder>();
            DependencyService.Register<PresentationSelecter>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
