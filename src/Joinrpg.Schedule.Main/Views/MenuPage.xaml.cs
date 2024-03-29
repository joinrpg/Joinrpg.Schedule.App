﻿using Joinrpg.Schedule.Main.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Joinrpg.Schedule.Main.PresentationMode;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Joinrpg.Schedule.Main.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage => Application.Current.MainPage as MainPage;
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.All, Title="Все мероприятия" },
                new HomeMenuItem {Id = MenuItemType.Today, Title="Сегодня будут" },
                new HomeMenuItem {Id = MenuItemType.NowGoing, Title="Сейчас идут" },
                new HomeMenuItem {Id = MenuItemType.Tomorrow, Title="Завтра" },
                new HomeMenuItem {Id = MenuItemType.About, Title="О приложении" },
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = ((HomeMenuItem)e.SelectedItem).Id;
                RootPage.PresentationMode = false;
                await RootPage.NavigateFromMenu(new NavigationArgs() {Type = id});
            };
        }
    }
}