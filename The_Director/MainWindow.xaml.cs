using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using The_Director.Data;
using System.Linq;
using Steamworks;
using The_Director.Utils;
using System.Diagnostics;

namespace The_Director
{
    public partial class MainWindow : MetroWindow
    {
        public List<NavMenu> NavMenus { get; set; } = new();

        private readonly Dictionary<string, UserControl> NavDictionarys = new();
        public static Window MainWindowInstance { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            MainWindowInstance = this;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SteamClient.Init(Globals.L4D2AppID);
            }
            catch (Exception)
            {
                Functions.TryOpenMessageWindow(-1);
                Application.Current.Shutdown();
                return;
            }

            DataContext = this;
            NavMenus.Add(new() { Title = "导演脚本", ViewName = "RescueScriptPage" , Index = 0});
            NavMenus.Add(new() { Title = "软件设置", ViewName = "SettingsPage", Index = 1 });
            NavMenus.Add(new() { Title = "关于作者", ViewName = "AboutPage", Index = 2 });
            NavMenus.ForEach(menu =>
            {
                var type = Type.GetType($"The_Director.Windows.{menu.ViewName}");
                NavDictionarys.Add(menu.ViewName, Activator.CreateInstance(type) as UserControl);
            });
        }

        private void MenuReselected(object sender, SelectionChangedEventArgs e)
        {
            WindowFrame.Content = NavDictionarys.Values.ElementAt(Menu.SelectedIndex);
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void WindowClosed(object sender, EventArgs e)
        {
            
        }
    }
}
