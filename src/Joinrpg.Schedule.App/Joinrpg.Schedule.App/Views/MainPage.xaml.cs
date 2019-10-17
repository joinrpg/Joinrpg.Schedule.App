using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Joinrpg.Schedule.App.Models;
using Joinrpg.Schedule.App.ViewModels;

namespace Joinrpg.Schedule.App.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Today, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                var itemType = (MenuItemType) id;
                switch (itemType)
                {
                    case MenuItemType.Today:
                        MenuPages.Add(id, new NavigationPage(new SchedulePage(ProgramItemsSelectMode.Today)));
                        break;
                    case MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case MenuItemType.NowGoing:
                        MenuPages.Add(id, new NavigationPage(new SchedulePage(ProgramItemsSelectMode.NowGoing)));
                        break;
                    case MenuItemType.Tomorrow:
                        MenuPages.Add(id, new NavigationPage(new SchedulePage(ProgramItemsSelectMode.Tomorrow)));
                        break;
                    case MenuItemType.ProudlyPresent:
                        throw  new NotImplementedException();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}