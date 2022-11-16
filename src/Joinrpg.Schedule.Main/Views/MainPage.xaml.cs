using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

using Joinrpg.Schedule.Main.Models;
using Joinrpg.Schedule.Main.PresentationMode;
using Joinrpg.Schedule.Main.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Joinrpg.Schedule.Main.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : FlyoutPage
    {
        Dictionary<MenuItemType, NavigationPage> MenuPages = new Dictionary<MenuItemType, NavigationPage>();
        readonly Timer timer;
        private bool _presentationMode;

        private async void Timer_Tick(object state)
        {
            var args = await Selector.GetNextPresentation();

            MainThread.BeginInvokeOnMainThread(async () => await NavigateFromMenu(args) );
        }

        public MainPage()
        {
            InitializeComponent();

            //MasterBehavior = MasterBehavior.Popover;

            Selector = DependencyService.Get<PresentationSelecter>();

            MenuPages.Add((int)MenuItemType.Today, (NavigationPage)Detail);
            timer = new Timer(Timer_Tick);
        }

        public PresentationSelecter Selector { get; set; }

        public bool PresentationMode
        {
            get => _presentationMode;
            set
            {
                if (_presentationMode == value)
                {
                    return;
                }
                _presentationMode = value;
                if (_presentationMode)
                {
                    timer.Change(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(10));
                }
                else
                {
                    timer.Change(-1, -1);
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            PresentationMode = false;
        }

        public async Task NavigateFromMenu(NavigationArgs args)
        {
            NavigationPage newPage;
            if (args.ExtraAgrument != null)
            {
                // do not cache pages with ids for now TODO improve
                newPage = await ConstructPage(args);

            }
            else
            {
                if (!MenuPages.ContainsKey(args.Type))
                {
                    newPage = await ConstructPage(args);
                    MenuPages.Add(args.Type, newPage);
                }

                newPage = MenuPages[args.Type];
            }

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (DeviceInfo.Platform == DevicePlatform.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

        private static async Task<NavigationPage> ConstructPage(NavigationArgs args)
        {
            NavigationPage newPage;
            //switch (args.Type)
            //{
            //    case MenuItemType.Today:
            //        newPage = new NavigationPage(new SchedulePage(ProgramItemsSelectMode.Today));
            //        break;
            //    case MenuItemType.About:
            //        newPage = new NavigationPage(new AboutPage());
            //        break;
            //    case MenuItemType.NowGoing:
            //        newPage = new NavigationPage(new SchedulePage(ProgramItemsSelectMode.NowGoing));
            //        break;
            //    case MenuItemType.Tomorrow:
            //        newPage = new NavigationPage(new SchedulePage(ProgramItemsSelectMode.Tomorrow));
            //        break;
            //    case MenuItemType.ProudlyPresent:
            //        var itemDetailViewModel = new ItemDetailViewModel();
            //        await itemDetailViewModel.InitializeAsync(args.ExtraAgrument);
            //        var itemDetailPage = new ItemDetailPage(itemDetailViewModel);
            //        newPage = new NavigationPage(itemDetailPage);
            //        break;
            //    case MenuItemType.All:
            //        newPage = new NavigationPage(new SchedulePage(ProgramItemsSelectMode.All));
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //}

            return null;
        }
    }
}