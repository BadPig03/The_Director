using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using System.Linq;
using Steamworks;
using The_Director.Utils;

namespace The_Director
{
    public partial class MainWindow : MetroWindow
    {
        public List<NavMenu> NavMenus { get; set; } = new();
        public static Window MainWindowInstance { get; private set; }

        private readonly Dictionary<string, UserControl> NavDictionarys = new();

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
                Environment.Exit(0);
                return;
            }

            DataContext = this;
            NavMenus.Add(new() { Title = "导演脚本", ViewName = "RescueScriptPage" , Index = 0 });
            NavMenus.Add(new() { Title = "其他处理", ViewName = "OtherProcessPage", Index =1 });
            NavMenus.Add(new() { Title = "软件设置", ViewName = "SettingsPage", Index = 2 });
            NavMenus.Add(new() { Title = "关于作者", ViewName = "AboutPage", Index = 3 });
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
            e.Cancel = !Globals.CanShutdown;
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            
        }
    }
}
